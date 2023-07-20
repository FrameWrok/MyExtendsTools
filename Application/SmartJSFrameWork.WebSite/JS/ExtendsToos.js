var ExtendTools = {};

ExtendTools.IsEmail = String.prototype.IsEmail = function () {
    /// <summary>
    /// 判断是否是正确的电子邮件格式
    /// </summary>    
    /// <returns type="Bool"></returns>
    return /^\w+((-\w+)|(\.\w+))*\@\w+((\.|-)\w+)*\.\w+$/.test(this);
}
ExtendTools.IsPhone = String.prototype.IsPhone = function () {
    /// <summary>
    /// 判断是否是正确的座机格式
    /// </summary>    
    /// <returns type="Bool"></returns>
    return /^\d{7,8}$/.test(this) || /^\d{7,8}[-转#]\d{1,6}$/.test(this) || /^\d{3,4}-\d{7,8}$/.test(this) || /^\d{3,4}-\d{7,8}[-转#]\d{1,6}$/.test(this);
}
ExtendTools.IsMobilePhone = String.prototype.IsMobilePhone = function () {
    /// <summary>
    /// 判断是否是正确的手机号格式
    /// </summary>    
    /// <returns type="Bool"></returns>
    return /^[1]{1}[0-9]{10}$/.test(this);
}
ExtendTools.IsZipCode = String.prototype.IsZipCode = function () {
    /// <summary>
    /// 判断是否是正确的邮政编码格式
    /// </summary>    
    /// <returns type="Bool"></returns>
    return /^\d{6}$/.test(this);
}
ExtendTools.IsLetterNumber = String.prototype.IsLetterNumber = function () {
    /// <summary>
    /// 验证是不是只是字母和数字的组合
    /// </summary>
    /// <returns type="Bool">验证的结果：true＝是；false＝不是</returns>
    return /^[A-Za-z0-9]+$/.test(this);
}
ExtendTools.IsLetterNumberUnderline = String.prototype.IsLetterNumberUnderline = function () {
    /// <summary>
    /// 验证是不是字符、数字或者下划线的组合
    /// </summary>
    /// <returns type="Bool">验证的结果：true＝是；false＝不是</returns>
    return /^[a-zA-Z0-9_]+$/.test(this);
}
ExtendTools.IsLetter = String.prototype.IsLetter = function () {
    /// <summary>
    /// 是否是只有26个英文字母组成的字符串
    /// </summary>
    /// <returns type="Bool">验证的结果：true＝是；false＝不是</returns>
    return /^[A-Za-z]+$/.test(this);
}
ExtendTools.IsUpper = String.prototype.IsUpper = function () {
    /// <summary>
    /// 验证是不是只有大写字母组成的字符串
    /// </summary>        
    /// <returns type="Bool">验证的结果：true＝是；false＝不是</returns>
    return /^[A-Z]+$/.test(this);
}
ExtendTools.IsLower = String.prototype.IsLower = function () {
    /// <summary>
    /// 验证是不是只有小写字母组成的字符串
    /// </summary>        
    /// <returns type="Bool">验证的结果：true＝是；false＝不是</returns>
    return /^[a-z]+$/.test(this);
}
ExtendTools.IsChiness = String.prototype.IsChiness = function () {
    /// <summary>
    /// 验证是不是中文字符串
    /// </summary>
    /// <returns type="Bool">验证的结果：true＝是；false＝不是</returns>
    return /[\u4e00-\u9fa5]/.test(this);
}
ExtendTools.IsNumber = String.prototype.IsNumber = function () {
    /// <summary>
    /// 验证是否是纯数字组合
    /// </summary>
    /// <returns type="Bool">验证结果：True=是，False=否</returns>
    return /^[0-9]+$/.test(this);
}
ExtendTools.IsNoNegativeInteger = String.prototype.IsNoNegativeInteger = function () {
    /// <summary>
    /// 验证是否是非负整数，包含正整数和0（0.0,1,1.00,）
    /// </summary>
    /// <returns type="Bool">验证结果：true=是，false=否</returns>
    return /^\\d+$/.test(this);
}
ExtendTools.IsPositiveInteger = String.prototype.IsPositiveInteger = function () {
    /// <summary>
    /// 验证是否是正整数，0不包含在内
    /// </summary>
    /// <returns type="Bool">验证结果：true=是，false=否</returns>
    return /^[0-9]*[1-9][0-9]*$/.test(this);
}
ExtendTools.IsPositiveDouble = String.prototype.IsPositiveDouble = function () {
    /// <summary>
    /// 验证是否是正浮点数
    /// </summary>
    /// <returns type="Bool">验证结果：true=是，false=否</returns>
    return /^(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*))$/.test(this);
}
ExtendTools.IsDouble = String.prototype.IsDouble = function () {
    /// <summary>
    /// 验证是否是浮点数
    /// </summary>
    /// <returns> type="Bool"验证结果：true=是，false=否</returns>
    return /^(-?\d+)(\.\d+)?$/.test(this);
}
ExtendTools.IsDecimalDigits = String.prototype.IsDecimalDigits = function (len) {
    /// <summary>
    /// 验证小数位数是不是指定的长度
    /// </summary>
    /// <param name="length">指定的长度</param>
    /// <returns type="Bool">验证的结果：true＝在范围内；false＝不在范围内</returns>
    var reg = new RegExp("^[0-9]+(.[0-9]{" + length + "})$");
    return reg.test(this);
}
ExtendTools.ValidatorTool = {};
ExtendTools.ValidatorTool = {
    ///获取当前光标在文本中的位置
    getCursorPosition: function (control) {
        /// <summary>
        /// 获取输入控件中光标的位置
        /// </summary>        
        /// <returns type="Bool">输入控件中光标的位置</returns>
        if (document.selection) {
            control.focus();
            var oSel = document.selection.createRange();
            oSel.moveStart('character', -control.value.length);
            return oSel.text.length;
        }
        else (control.selectionStart)
        {
            return control.selectionStart;
        }
    }
}
ExtendTools.ValidatorTool.NumberValidator = {
    numberIsAllowed: function (keycode, integerValue, decimalValue, control) {
        /// <summary>
        /// 获取该按键是否已经处理，如果已经处理则不再允许输入，即返回true
        /// </summary>        
        /// <returns type="Bool">输入控件中光标的位置</returns>
        var arr = new Array(8, 9, 13, 17, 35, 36, 37, 38, 39, 40, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57);
        for (i = 0; i < arr.length; i++) {
            if (arr[i] == keycode) {
                var enterValue = '';
                if (control.value != '') {
                    var cursorPos = ExtendTools.ValidatorTool.getCursorPosition(control);
                    if (cursorPos == control.value.length) enterValue = $.trim(control.value + String.fromCharCode(keycode));
                    else enterValue = $.trim(control.value.substring(0, cursorPos) + String.fromCharCode(keycode) + control.value.substring(cursorPos));
                }
                else enterValue = String.fromCharCode(keycode);
                if (enterValue.length > 0) {
                    var indexVal = enterValue.length;
                    if (enterValue.indexOf('.') != -1) {
                        var arr = new Array(2);
                        arr = enterValue.split('.');
                        var thisIntegerValue = arr[0];
                        var thisDecimalValue = arr[1];
                        if (thisIntegerValue.length <= integerValue) {
                            if (thisDecimalValue.length <= decimalValue) return true;
                            else return false;
                        }
                        else return false;
                    }
                    else {
                        var thisIntegerValue = enterValue.length;
                        if (thisIntegerValue <= integerValue) return true;
                        else return false;
                    }
                }
            }
        }
        return false;
    },
    checkString: function (control, isAllowDecimal) {
        var $control = $(control);
        isAllowDecimal = (isAllowDecimal || $control.attr("IsAllowDecimal")) || false; ////是否允许小数
        var isAllowNegative = ($control.attr("IsAllowNegative")) || false;////是否允许负数
        var integerLength = ($control.attr("IntegerLength")) || 10; ////整数位长度
        var decimalLength = isAllowDecimal ? (($control.attr("DecimalLength")) || 2) : 0; ////小数位长度
        var res = '';
        var decimalSeparatorUsed = false;
        var intAllow = '1234567890';
        for (i = 0; i < $control.val().length; i++) {
            if (i == 0 && $control.val().charAt(0) == '-' && isAllowNegative) {
                res = '-';
            }
            else {
                if (intAllow.indexOf($control.val().charAt(i)) > -1) {
                    res += $control.val().charAt(i);
                }
                else {
                    if ($control.val().charAt(i) == "." && decimalSeparatorUsed == false && isAllowDecimal) {
                        res += $control.val().charAt(i);
                        decimalSeparatorUsed = true;
                    }
                }
            }
        }
        $control.val(res == "" ? 0 : res);
    },
    ValidatorNumberInput: function (e, textcontrol, isAllowDecimal) {
        /// <summary>
        /// 使用方法 在输入控件中 onkeypress = "return ValidatorNumberInput(event,this)";
        /// 如果允许小数，请在输入控件添加 IsAllowDecimal = "true" 特性;
        /// 如果允许负数，请在输入控件添加 IsAllowNegative =  "true" 特性;
        /// 如果限制整数位的长度，请在输入控件添加 IntegerLength = "10" 特性，10代表要限制输入的最大长度;
        /// 如果限制小数位的长度，请在输入控件添加 DecimalLength = "2" 特性，2代表小数位的长度;
        /// </summary>
        /// <param name="textcontrol">输入控件</param>
        /// <returns type="Bool"></returns>
        var $control = $(textcontrol);
        isAllowDecimal = (isAllowDecimal || $control.attr("IsAllowDecimal")) || false; ////是否允许小数
        var isAllowNegative = ($control.attr("IsAllowNegative")) || false;////是否允许负数
        var integerLength = ($control.attr("IntegerLength")) || 10; ////整数位长度
        var decimalLength = isAllowDecimal ? (($control.attr("DecimalLength")) || 2) : 0; ////小数位长度
        var keycode = e.keyCode ? e.keyCode : e.charCode;
        var dotKeyCode = 46; // . 的 keycode
        var evt = window.event ? event : e;
        if (evt.ctrlKey || evt.altKey || evt.shiftKey) {
            return true;
        }
        switch (keycode) {
            case 0:
                return true;
            case 109:
            case 189:
                if (ExtendTools.ValidatorTool.getCursorPosition(textcontrol) == 0 && negative) return true;
                else return false;
            case 46:
                {
                    if (textcontrol.value.replace('-', '').length > 0
                        && ((ExtendTools.ValidatorTool.getCursorPosition(textcontrol) > 0 && textcontrol.value.indexOf('-') == -1) || (ExtendTools.ValidatorTool.getCursorPosition(textcontrol) > 1 && textcontrol.value.indexOf('-') > -1))
                        && textcontrol.value.indexOf(".") == -1
                        && isAllowDecimal
                        && textcontrol.value.substring(ExtendTools.ValidatorTool.getCursorPosition(textcontrol)).length <= decimalLength)
                        return true;
                    else
                        return false;
                } break;
            default:
                return ExtendTools.ValidatorTool.NumberValidator.numberIsAllowed(keycode, integerLength, decimalLength, textcontrol);
                break;
        }
    }

}
ExtendTools.SetObjectPropertyValue = function (o, propertyName, value, type) {
    if (o[propertyName]) {
        if (!o[propertyName].push) {
            o[propertyName] = [o[propertyName]];
        }
        o[propertyName].push($.trim(value || ''));
    } else {
        o[propertyName] = $.trim(value || '');
    }
}
$.ToJson = function (selector) {
    /// <summary>
    /// 将选择器内的控件或者传入的选择器内的子控件序列化为json对象返回  
    /// </summary> 
    var o = {};
    var formArray = [];
    $(selector).find("input:checkbox:checked,input:text,input:hidden,input:radio:checked,textarea").each(function () {
        formArray.push({ name: $(this).attr("name"), value: $(this).val(), type: $(this)[0].type });
    });
    $.each(formArray, function () {
        if (this.name.indexOf('.') > 0) {
            var pArray = this.name.split(".");
            if (pArray.length == 2) {
                if (o[pArray[0]] == undefined)
                    o[pArray[0]] = {};
                ExtendTools.SetObjectPropertyValue(o[pArray[0]], pArray[1], this.value);
            }
            if (pArray.length == 3) {
                if (o[pArray[0]] == undefined)
                    o[pArray[0]] = {};
                if (o[pArray[0]][pArray[1]] == undefined)
                    o[pArray[0]][pArray[1]] = {};
                if (this.type == "checkbox" && o[pArray[0]][pArray[1]][pArray[2]] == undefined)
                    o[pArray[0]][pArray[1]][pArray[2]] = [];
                ExtendTools.SetObjectPropertyValue(o[pArray[0]][pArray[1]], pArray[2], this.value);
            }
        } else {
            if (this.type == "checkbox" && o[this.name] == undefined)
                o[this.name] = [];
            ExtendTools.SetObjectPropertyValue(o, this.name, this.value, this.type);
        }
    });
    return o;
}
$.getRequestParameter = function (parametName, localtionUrl) {
    var re = new RegExp(parametName + "=([^\&]*)", "i");
    var a = re.exec(localtionUrl);
    if (a == null)
        return null;
    return $.trim(a[1]);
}
function InitGridLineHoverStyle() {
    /// <summary>
    /// 初始化DataGrid的行样式   
    /// </summary>        
    $(".table>tbody>tr").click(function () {
        if ($(this).hasClass("active"))
            $(this).removeClass("active");
        else {
            $(this).parent("tbody").children("tr").removeClass("active");
            $(this).addClass("active");
        }
    });
    $(".table>tbody>tr").dblclick(function () {
        if ($(this).hasClass("active")) {
            $(this).removeClass("active");
        }
        else {
            $(this).parent("tbody").children("tr").removeClass("active");
            $(this).addClass("active");
        }
        var $radio = $(this).children("td").eq(1).children("input[type='radio']").eq(0);
        var $check = $(this).children("td").eq(1).children("input[type='checkbox']").eq(0)
        if ($radio != undefined && ($radio.attr("checked") == undefined || $radio.attr("checked") == false))
            $radio.attr("checked", "checked");
        if ($check != undefined) {
            if ($check.attr("checked") == undefined || $check.attr("checked") == false) $check.attr("checked", "checked");
            else $check.removeAttr("checked");
        }
    });
}
function InitInputValidateContorl() {
    /// <summary>
    /// 初始化输入验证控件的行样式   
    /// </summary>
    $("input[data-type='Int']").css("ime-mode", "disabled").unbind().keypress(function (e) {
        return ExtendTools.ValidatorTool.NumberValidator.ValidatorNumberInput(e, this, false);
    }).change(function (e) { ExtendTools.ValidatorTool.NumberValidator.checkString(this, false); });
    $("input[data-type='Decimal']").css("ime-mode", "disabled").unbind().keypress(function (e) {
        return ExtendTools.ValidatorTool.NumberValidator.ValidatorNumberInput(e, this, true);
    }).change(function (e) { ExtendTools.ValidatorTool.NumberValidator.checkString(this, true); });;
}
(function (root, factory) {
    //amd
    if (typeof define === 'function' && define.amd) {
        define(['$'], factory);
    } else if (typeof exports === 'object') { //umd
        module.exports = factory();
    } else {
        root.Loading = factory(window.Zepto || window.jQuery || $);
    }
})(this, function ($) {
    var Loading = function () { };
    Loading.prototype = {
        loadingTpl: '<div class="ui-loading"><div class="ui-loading-mask"></div><i></i></div>',
        stop: function () {
            //var content = $(this.target);
            //this.loading.remove();
            this.loading.hide();
        },
        start: function () {
            var _this = this;
            var target = _this.target;
            var content = $(target);
            var loading = this.loading;
            if (!loading) {
                loading = $(_this.loadingTpl);
                $('body').append(loading);
            }
            this.loading = loading;
            var ch = $(content).outerHeight();
            var cw = $(content).outerWidth();
            if ($(target)[0].tagName == "HTML") {
                ch = Math.max($(target).height(), $(window).height());
                cw = Math.max($(target).width(), $(window).width());
            }
            //console.log(cw,ch)
            loading.height(ch).width(cw);
            loading.find('div').height(ch).width(cw);
            if (ch < 100) {
                loading.find('i').height(ch).width(ch);
            }
            var offset = $(content).offset();
            loading.css({
                top: offset.top,
                left: offset.left
            });
            var icon = loading.find('i');
            var h = ch,
				w = cw,
				top = 0,
				left = 0;
            if ($(target)[0].tagName == "HTML") {
                h = $(window).height();
                w = $(window).width();
                top = (h - icon.height()) / 2 + $(window).scrollTop();
                left = (w - icon.width()) / 2 + $(window).scrollLeft();
            } else {
                top = (h - icon.height()) / 2;
                left = (w - icon.width()) / 2;
            }
            icon.css({
                top: top,
                left: left
            });
            this.loading.show();
        },
        init: function (settings) {
            settings = settings || {};
            this.loadingTpl = settings.loadingTpl || this.loadingTpl;
            this.target = settings.target || 'html';
            this.bindEvent();
        },
        bindEvent: function () {
            var _this = this;
            $(this.target).on('stop', function () {
                _this.stop();
            });
        }
    }
    return Loading;
});