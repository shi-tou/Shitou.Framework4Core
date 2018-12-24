/*--------------------------------
基于WebUploader构建公用的单图、多图上传脚本
--------------------------------*/

//多图：初始化上传插件（type-图片类别 target-目标Id）
function InitBatchUploader(PicType, targetID) {

    //缩略图大小
    var thumbnailWidth = 110, thumbnailHeight = 110, fileCount = 0;

    //上传图片队伍
    var $queuelist = $('#queuelist');
    //上传按钮
    var $filePicker = $('#filePicker');

    // WebUploader实例
   var $uploader = WebUploader.create({
       pick: {
           id: '#filePicker',
           label: '点击选择图片'
       },
       method :'POST',
       formData: {
           'PicType': PicType,
           'targetID':targetID
       },
       //指定Drag And Drop拖拽的容器，如果不指定，则不启动
       //dnd: '#dndArea',
       //指定监听paste事件的容器，如果不指定，不启用此功能。此功能为通过粘贴来添加截屏的图片
       //paste: '#uploader',
       swf: '/lib/webuploader/Uploader.swf',
       //是否要分片处理大文件上传
       chunked: false,
       //如果要分片，分多大一片？ 默认大小为5M.
       chunkSize: 512 * 1024,
       server: '/Image/UploadBatchImage',
       // runtimeOrder: 'flash',
       //指定接受哪些类型的文件。 由于目前还有ext转mimeType表，所以这里需要分开指定。
       accept: {
           title: 'Images',
           extensions: 'gif,jpg,jpeg,bmp,png',
           mimeTypes: 'image/*'
       },

       // 禁掉全局的拖拽功能。这样不会出现图片拖进页面的时候，把图片打开。
       disableGlobalDnd: true,
       //验证文件总数量, 超出则不允许加入队列
       fileNumLimit: 5,
       //验证文件总大小是否超出限制, 超出则不允许加入队列
       fileSizeLimit: 5 * 1024 * 1024,    // 5M
       //验证单个文件大小是否超出限制, 超出则不允许加入队列。
       fileSingleSizeLimit: 1 * 1024 * 1024    // 1M
   });

    //当文件被加入队列之前触发，此事件的handler返回值为false，则此文件不会被添加进入队列。
    $uploader.on('beforeFileQueued', function (file) {
        $uploader.addButton({
            id: '#btnContainer',
            innerHTML: '选择文件'
        });
    });

    //当文件被加入队列以后触发
    $uploader.on('fileQueued', function (file) {
        var $li = $('<li id="' + file.id + '">'
                    + '<p class="queueImg"></p>'
                    + '</li>');
        var $btns = $('<div class="file-panel">'
                    + '<span class="cancel">删除</span>'
                    + '</div>').appendTo($li);
        var $wrap = $li.find('p.queueImg');
         //生成缩略图
         $uploader.makeThumb(file, function (error, src) {
             if (error) {
                 $wrap.text('不能预览');
                 return;
             }
             var img = $('<img src="' + src + '">');
             $wrap.empty().append(img);
         }, thumbnailWidth, thumbnailHeight);
        //鼠标进入事件
        $li.on('mouseenter', function () {
            $btns.stop().animate({ height: 30 });
        });
        //鼠标移出事件
        $li.on('mouseleave', function () {
            $btns.stop().animate({ height: 0 });
        });
        //删除上传列队事件
        $btns.on('click', 'span', function () {
            var index = $(this).index();
            switch (index) {
                case 0:
                    $uploader.removeFile(file);
                    return;
            }
        });
        //将图片加入队列
        $li.appendTo($queuelist);
        if (fileCount == 0) {
            var $fileUpload = $('<div id="fileUpload" class="webuploader-pick" style="margin-left:10px; background:#ed5565;">开始上传</div>');
            $fileUpload.appendTo($filePicker);
            $fileUpload.on('click', function () {
                $uploader.upload();
            });
        }
        fileCount++;
    });

    //当文件被移除队列后触发
    $uploader.on('fileDequeued', function (file) {
        var $li = $('#' + file.id);
        $li.off().find('.file-panel').off().end().remove();//移除li视图
        fileCount--;
        if (fileCount == 0) {
            $('#fileUpload').off().remove();
        }
    });

    //当文件上传成功时触发
    $uploader.on('uploadSuccess', function (file, response) {
        //response为ImageController返回的json数据
    });

    //当所有文件上传结束时触发。
    $uploader.on('uploadFinished', function (file) {
        layer.alert('上传完毕', { icon: 1, shadeClose: false, title: '操作提示' }, function () {
            location.reload();
        });
    });
}

//单图（品牌Logo,会员头像）
function InitSingleUploader(server, pick, PicType, callback) {
    var webUploader = WebUploader.create({
        // 选完文件后，是否自动上传。
        auto: true,
        // swf文件路径
        swf: '/Scripts/webuploader-0.1.5/Uploader.swf',
        // 文件接收服务端。
        server: server,
        //表单数据
        formData: { PicType: PicType },
        // 选择文件的按钮。可选。
        // 内部根据当前运行是创建，可能是input元素，也可能是flash.
        pick: pick,
        // 不压缩image, 默认如果是jpeg，文件上传前会压缩一把再上传！
        resize: false,
        // 只允许选择图片文件。
        accept: {
            title: 'Images',
            extensions: 'gif,jpg,jpeg,bmp,png',
            mimeTypes: 'image/*'
        }
    });
    webUploader.on('uploadSuccess', function (file, response) {
        if (callback != null)
            callback(file, response);
    });
    webUploader.on('uploadError', function (file) {
        layer.alert('上传失败', { icon: 2, shadeClose: false, title: '操作提示' });
    });
}