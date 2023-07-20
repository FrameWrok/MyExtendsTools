/**********************************************************






**********************************************************/
function Class(className) {
    var usrClass = function() {
        this.className = className;
    }
    return usrClass;
}
Object.prototype.extend = Class.prototype.extend = function() {
    if (arguments[0]) {
        for (var proto in arguments[0]) {
            this.prototype[proto] = arguments[0][proto];
        }
        return this.prototype;
    }
}
Array.extend({
    indexOf: function(o) {
        for (var i = 0; i < this.length; i++) {
            if (this[i] == o)
                return i;
        }
        return -1;
    },
    remove: function(o) {
        var index = this.indexOf(o);
        if (index != -1)
            this.splice(index, 1);
    },
    append: function(o) {
        this[this.length] = o;
    }
})

