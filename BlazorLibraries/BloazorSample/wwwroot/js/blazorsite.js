/// <reference path="../lib/jquery/dist/jquery.js" />
/// <reference path="../lib/bootstrap-datepicker/dist/js/bootstrap-datepicker.js" />
/// <reference path="../lib/bootstrap/dist/js/bootstrap.js" />

window.blazorjs = {
    datepicker: function (ref, viewMode) {

        $('#datepicker').datepicker('destroy');

        $('#datepicker').datepicker({
            minViewMode: viewMode
        }).on('changeDate', function (e) {
            let date = e.date;

            ref.invokeMethodAsync('NotifyChange', date.getFullYear(), date.getMonth(), date.getDate());
        });
    },
    fileReading: function (elem, componentInstance) {
        elem.addEventListener('change', function handleInputFileChange(event) {
            var myFile = elem.files[0];
            //var fileType = myFile.type;
            //var fileName = myFile.name;

            var reader = new FileReader();
            reader.onload = function (e) {
                var contents = e.target.result.toString();
                var result = {
                    name: myFile.name,
                    size: myFile.size,
                    type: myFile.type,
                    Content: contents
                };
                componentInstance.invokeMethodAsync('Notify', result);
                elem.value = '';
            };
            reader.readAsBinaryString(myFile);
        });
    },
    inputFilter: function () {
        
        (function ($) {
            $.fn.inputFilter = function (inputFilter) {
                return this.on("input keydown keyup mousedown mouseup select contextmenu drop", function () {
                    if (inputFilter(this.value)) {
                        this.oldValue = this.value;
                        this.oldSelectionStart = this.selectionStart;
                        this.oldSelectionEnd = this.selectionEnd;
                    } else if (this.hasOwnProperty("oldValue")) {
                        this.value = this.oldValue;
                        this.setSelectionRange(this.oldSelectionStart, this.oldSelectionEnd);
                    } else {
                        this.value = "";
                    }
                });
            };
        }(jQuery));

        $('#numberTextBox').inputFilter(function (value) {
            return /^-?\d*[.,]?\d*$/.test(value);
        });
    },
    alertMessageBox: function (delayTime) {
        $("#actionSuccessed").show().delay(delayTime).fadeToggle(500);
    },
    fileExcelReading: function (elem, componentInstance) {
        elem.addEventListener('change', function handleInputFileChange(event) {
            var myFile = elem.files[0];
            //var fileType = myFile.type;
            //var fileName = myFile.name;

            //var reader = new FileReader();
            //reader.onload = function (e) {
            //    var contents = e.target.result;
                var result = {
                    Content: myFile
                };
            //    componentInstance.invokeMethodAsync('NotifyExcel', elem);
            //    elem.value = '';
            //};
            //reader.readAsArrayBuffer(myFile);

            componentInstance.invokeMethodAsync('NotifyExcel', result);
            elem.value = '';
        });
    },

    objectInpu: function ( ref, obj) {

        ref.invokeMethodAsync('NotifyMessageFromJs', obj);
    },

    dragable: function () {
        dragElement(document.getElementById("mydiv"));

        function dragElement(elmnt) {
            var pos1 = 0, pos2 = 0, pos3 = 0, pos4 = 0;
            if (document.getElementById(elmnt.id + "header")) {
                /* if present, the header is where you move the DIV from:*/
                document.getElementById(elmnt.id + "header").onmousedown = dragMouseDown;
            } else {
                /* otherwise, move the DIV from anywhere inside the DIV:*/
                elmnt.onmousedown = dragMouseDown;
            }

            function dragMouseDown(e) {
                e = e || window.event;
                e.preventDefault();
                // get the mouse cursor position at startup:
                pos3 = e.clientX;
                pos4 = e.clientY;
                document.onmouseup = closeDragElement;
                // call a function whenever the cursor moves:
                document.onmousemove = elementDrag;
            }

            function elementDrag(e) {
                e = e || window.event;
                e.preventDefault();
                // calculate the new cursor position:
                pos1 = pos3 - e.clientX;
                pos2 = pos4 - e.clientY;
                pos3 = e.clientX;
                pos4 = e.clientY;
                // set the element's new position:
                elmnt.style.top = (elmnt.offsetTop - pos2) + "px";
                elmnt.style.left = (elmnt.offsetLeft - pos1) + "px";
            }

            function closeDragElement() {
                /* stop moving when mouse button is released:*/
                document.onmouseup = null;
                document.onmousemove = null;
            }
        }
    },

    jsCode: function (element) {
        var fu = element.innerText;

        // You cna user eval function or create an instance of Function
        eval(fu);

        //var func = new Function(fu);
        //func();
    }
};

var uint8ToBase64 = (function () {
    // Code from https://github.com/beatgammit/base64-js/
    // License: MIT
    var lookup = [];

    var code = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/';
    for (var i = 0, len = code.length; i < len; ++i) {
        lookup[i] = code[i];
    }

    function tripletToBase64(num) {
        return lookup[num >> 18 & 0x3F] +
            lookup[num >> 12 & 0x3F] +
            lookup[num >> 6 & 0x3F] +
            lookup[num & 0x3F];
    }

    function encodeChunk(uint8, start, end) {
        var tmp;
        var output = [];
        for (var i = start; i < end; i += 3) {
            tmp =
                ((uint8[i] << 16) & 0xFF0000) +
                ((uint8[i + 1] << 8) & 0xFF00) +
                (uint8[i + 2] & 0xFF);
            output.push(tripletToBase64(tmp));
        }
        return output.join('');
    }

    return function fromByteArray(uint8) {
        var tmp;
        var len = uint8.length;
        var extraBytes = len % 3; // if we have 1 byte left, pad 2 bytes
        var parts = [];
        var maxChunkLength = 16383; // must be multiple of 3

        // go through the array every three bytes, we'll deal with trailing stuff later
        for (var i = 0, len2 = len - extraBytes; i < len2; i += maxChunkLength) {
            parts.push(encodeChunk(
                uint8, i, (i + maxChunkLength) > len2 ? len2 : (i + maxChunkLength)
            ));
        }

        // pad the end with zeros, but make sure to not forget the extra bytes
        if (extraBytes === 1) {
            tmp = uint8[len - 1];
            parts.push(
                lookup[tmp >> 2] +
                lookup[(tmp << 4) & 0x3F] +
                '=='
            );
        } else if (extraBytes === 2) {
            tmp = (uint8[len - 2] << 8) + uint8[len - 1];
            parts.push(
                lookup[tmp >> 10] +
                lookup[(tmp >> 4) & 0x3F] +
                lookup[(tmp << 2) & 0x3F] +
                '='
            );
        }

        return parts.join('');
    };
})();