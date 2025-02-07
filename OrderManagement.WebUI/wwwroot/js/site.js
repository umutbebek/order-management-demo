// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var isObjectEmpty = function (obj) {

    return obj === null ||
        obj === undefined ||
        obj === "" ||
        obj === '' ||
        ($.type(obj) === 'string' && obj.trim().length === 0);
}

var isObjectTrue = function (obj) {

    return obj === 1 ||
        obj === "1" ||
        obj === "True" ||
        obj === "true" ||
        obj === true;
}

var getUrlVars = function () {
        var vars = [], hash;
        var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        for (var i = 0; i < hashes.length; i++) {
            hash = hashes[i].split('=');
            vars.push(hash[0]); //to get name before =
            if (hash[0] === "returnUrl") { //returnUrl must be always the last in the query!!!
                vars[hash[0]] = window.location.href.split("returnUrl=")[1]; //get the whole string as returnUrl!
                break;
            } else {
                vars[hash[0]] = hashes[i].slice(hashes[i].indexOf('=') + 1); //to take everything after first =
            }
        }
        return vars;
    }

var _apiPath = "https://localhost:7221/";
var call = function (options) {
    //path, data, type (POST, GET etc.), success, error, targetName, showLoading = true, stringify = true
    console.log("called ajax: " + options.path);
    console.log(JSON.stringify(options.data));

    if (!options.data)
        options.data = {};

    $.ajax({
        url: _apiPath + options.path,
        contentType: 'application/json',
        type: options.type,
        data: JSON.stringify(options.data),
        dataType: 'json',
        //cache: false,
        //async: true,
        success: function (result) {

            //console.log("ajax finished...");
            if (!window.isObjectTrue(result.HasError)) {

                console.log("returned response from: " + options.path);
                console.log(result);
                if (options.success)
                    options.success(result);

            } else if (result.Error === "429") {

                    //it is "to many request error", so call it again
                    setTimeout(() => {
                        post(options);
                    }, 1000);

            } else {

                if (options.error)
                    options.error(result.Error);
                else {
                    window.alert(result.Error);
                }
            }
        },
        error: function (xhr, status, httpError) {

            if (options.error)
                options.error(status + " " + httpError);
            else
                window.alert('Status: ' + status + ' HttpError: ' + httpError);
        }
    });
};