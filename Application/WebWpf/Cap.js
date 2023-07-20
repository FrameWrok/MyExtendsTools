var carblobbase64 = "123";
var scale = 1.5; //定义任意放大倍数 支持小数
var cntElem = $("html")[0]; //document.getElementById("root");// $("#wrapper")[0];
//var shareContent = cntElem;//需要截图的包裹的（原生的）DOM 对象
var width = cntElem.offsetWidth; //获取dom 宽度
var height = cntElem.offsetHeight; //获取dom 高度
var canvas = document.createElement("canvas"); //创建一个canvas节点
canvas.width = width * scale; //定义canvas 宽度 * 缩放
canvas.height = height * scale; //定义canvas高度 *缩放
//canvas.getContext("2d").scale(scale, scale); //获取context,设置scale 
var opts = {
    scale: scale, // 添加的scale 参数
    canvas: canvas, //自定义 canvas
    logging: true, //日志开关，便于查看html2canvas的内部执行流程
    width: width, //dom 原始宽度
    height: height,
    useCORS: true, // 【重要】开启跨域配置
    dpi: 500,
};
function generate() {
    html2canvas(cntElem, opts).then(function (canvas) {
        var context = canvas.getContext('2d');
        context.mozImageSmoothingEnabled = false;
        context.webkitImageSmoothingEnabled = false;
        context.msImageSmoothingEnabled = false;
        context.imageSmoothingEnabled = false;
        carblobbase64 = canvas.toDataURL("image/jpeg", 1.0);
    });
}
generate();
