const toast = (function () {

    const callIziToast = function (method, args) {
        message = messages[args[0]] || args[0];
        const options = Object.assign({
            message: message,
            title: args[1],
            position: 'topCenter'
        }, args[2]);
        iziToast[method].apply(iziToast, [ options ]);
    }

    return {
        error: function(message, title = "", options = {}) {
            callIziToast('error', arguments);
        },
        success: function(message, title = "", options = {}) {
            callIziToast('success', arguments);
        },
    };

})();


const cookiesHelper = (function() {

    const getCookie = function(name) {
        var value = "; " + document.cookie;
        var parts = value.split("; " + name + "=");
        if (parts.length == 2) return parts.pop().split(";").shift();
    }

    const deleteCookie = function(name) {
        document.cookie = name + '=; expires=Thu, 01 Jan 1970 00:00:01 GMT;path=/;';
    }

    return {
        getCookie: getCookie,
        deleteCookie: deleteCookie
    };

})();