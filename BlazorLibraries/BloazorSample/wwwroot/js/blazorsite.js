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
    }
};