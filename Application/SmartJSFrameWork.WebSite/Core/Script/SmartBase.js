//类
function Class(className) {
    var usrClass = function() {        
        if (!Class.extending) {
            if (this.structor)
                return this.structor.apply(this, arguments);
            else
                throw new Error("class \"" + className + "\" structor is not defined");
        }
    };
    usrClass._className = className;
    usrClass.prototype = new Object();    
    this.extend(usrClass, this);
    return usrClass;
}
window.extend = function(target, property_object) {
    if (!o) return;
    if (!property_object) return;
    for (var proto in property_object) {
        o[proto] = property_object[proto];
    }
    return o;
}
Class.prototype.inherit = function(p) {
    this.Super = p.prototype;
    Class.extending = true;
    this.prototype = new p();
    delete Class.extending;
    this.prototype.constructor = p;
}

//类的扩展方法
Object.extend = Class.prototype.extend = function() {
    var args = arguments;
    if (!args[1])
        args = [this, args[0]];
    for (var property in args[1])
        args[0][property] = args[1][property];
    return args[0];
}
//命名空间
function ns(nameSpace) {
    if (!nameSpace)
        return null;
    var arr = nameSpace.split('.');
    var o = window[arr[0]] = window[arr[0]] || new Class(arr[0]);
    for (var i = 1; i < arr.length; i++) {
        o = o[arr[i]] = o[arr[i]] || new Class(arr[i]);
    }
    return o;
}
ns("system");
ns("EventManager");
EventManager.extend({
    addListener: function(el, name, fn, capture) {
        if (window.addEventListener) {
            el.addEventListener(name, fn, capture);
        }
        else if (window.attachEvent) {
            el.attachEvent(name, fn);
        }
    },
    removeListener: function(el, name, fn, capture) {
        if (window.removeEventListener) {
            el.removeEventListener(name, fn, capture);
        }
        else if (window.detachEvent) {
            el.detachEvent(name, fn);
        }
    },
    on: this.addListener,
    un: this.removeListener
})
system.extend({
    isNumber: function(o) {
        return typeof o == "number";
    },
    isFunction: function(o) {
        return typeof o == "function";
    },
    isArray: function(o) {
        return o && o instanceof Array;
    },
    isString: function(o) {
        return typeof o == "string";
    },
    getDomLeft: function(Element) {
        if (!Element)
            throw new Error("getDomLeft parameter Element is null");
        var element = Element;
        var valueL = 0;
        do {
            valueL += element.offsetLeft || 0;
            if (element.offsetParent == document.body)
                break;
        }
        while (element = element.offsetParent);
        element = Element || this.dom;
        do {
            if (element.tagName == 'BODY') {
                valueL -= element.scrollLeft || 0;
            }
        }
        while (element = element.parentNode);
        return valueL;
    },
    getDomTop: function(Element) {
        if (!Element)
            throw new Error("getDomLeft parameter Element is null");
        var e = Element;
        var top = 0;
        do {
            top += e.offsetTop || 0;
            if (e.offsetParent == document.body)
                break;
        } while (e = e.offsetParent);
        e = Element;
        do {
            if (e.tagName == "BODY")
                top -= e.scorllTop || 0;
        } while (e = e.parentNode)
        return top;
    },
    EventManager: EventManager
})
/**********************  Array扩展   *****************************/
//得到某个元素在数组中的索引
Array.prototype.indexOf = function(o) {
    for (var i = 0; i < this.length; i++) {
        if (this[i] == o)
            return i;
    }
    return -1;
}
//移除数组中的某个元素
Array.prototype.remove = function(o) {
    var index = this.indexOf(o);
    if (index != -1)
        this.splice(index, 1);
}
//移除数组中的某个角标的元素
Array.prototype.removeIndex = function(index) {
    this.splice(index, 1);
}
//将元素添加到数组的末尾
Array.prototype.append = function(o) {
    this[this.length] = o;
}
//将某数组添加到数组的末尾
Array.prototype.appendArray = function(arr) {
    for (var o in arr) {
        this[this.length] = o;
    }
}
/**********************  string扩展   *****************************/
//模仿后台cs的format
String.format = function(format, args) {
    var argss = [];
    if (arguments.length > 1) {
        argss = Array.apply(this, arguments);
        argss.shift();
    }
    return format.replace(/\{(\d+)\}/g, function(m, i) { return argss[i]; });
}
String.prototype.format = function(args) {
    var argss = Array.apply(this, arguments);
    return this.replace(/\{(\d+)\}/g, function(m, i) { return argss[i]; });
}
//模仿后台的去除左右空格
String.prototype.trim = function() {
    var re = /^\s+|\s+$/g;
    return this.replace(re, "");
}
Object.extend(String,{
    aa: function() {

    }
})
/**********************  function扩展   *****************************/
Object.extend(Function.prototype,{
    createCallBack: function(/*....args...**/) {
        var inputParameters = arguments;
        var e = this;
        return function() {
            return e.apply(window, inputParameters);
        }
    },
    createDelegete: function(obj, args, appendArgs) {
        var e = this;
        return function() {
            var callArgs = args || arguments;
            if (appendArgs === true) {
                callArgs = Array.prototype.slice.call(arguments, 0);
                callArgs = callArgs.concat(args);
            } else if (!isNaN(appendArgs)) {
                callArgs = Array.prototype.slice.call(arguments, 0);
                var applyArgs = [appendArgs, 0].concat(args);
                Array.prototype.splice.apply(callArgs, applyArgs);
            }
            return e.apply(obj || window, callArgs);
        }
    }
})