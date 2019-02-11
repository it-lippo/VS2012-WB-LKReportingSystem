
var confirmed = false;
function BootboxConfirm(message, sender) {
    if (confirmed) { return true; }

    bootbox.confirm(message, function (result) {
        if (result) {
            if (sender != null) {
                confirmed = true;
                sender.click();
                confirmed = false;
            }
        }
    });

    return false;
}


function BootboxAlert(message) {
    bootbox.alert(message);
}


function ShowNotificationGreen(message) {
    var notification = new NotificationFx({
        message: '<p>' + message + '</p>',
        layout: 'growl',
        effect: 'scale',
        warna: 'green',
        type: 'notice', // notice, warning, error or success
        onClose: function () {
            bttn.disabled = false;
        }
    });

    // show the notification
    notification.show();
}

function ShowNotificationYellow(message) {
    var notification = new NotificationFx({
        message: '<p>' + message + '</p>',
        layout: 'growl',
        effect: 'scale',
        warna: 'yellow',
        type: 'notice', // notice, warning, error or success
        onClose: function () {
            bttn.disabled = false;
        }
    });

    // show the notification
    notification.show();
}

function ShowNotificationRed(message) {
    var notification = new NotificationFx({
        message: '<p>' + message + '</p>',
        layout: 'growl',
        effect: 'scale',
        warna: 'red',
        type: 'notice', // notice, warning, error or success
        onClose: function () {
            bttn.disabled = false;
        }
    });

    // show the notification
    notification.show();
}

function isNumberKeyWithDecimal(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 44 || charCode > 57) && charCode != 46)
        return false;

    return true;
}
function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 44 || charCode > 57))
        return false;

    return true;
}
function AvoidSpace() {
    if (event.keyCode == 32) {
        event.returnValue = false;
        return false;
    }
}

function FormatCurrency(Num) { //function to add commas to textboxes
    Num += '';
    Num = Num.replace(',', ''); Num = Num.replace(',', ''); Num = Num.replace(',', '');
    Num = Num.replace(',', ''); Num = Num.replace(',', ''); Num = Num.replace(',', '');
    x = Num.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1))
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    return x1 + x2;
}


var confirmed = false;
function runConfirm(message, controlID) {
    debugger;
    if (confirmed) { return true; }

    bootbox.confirm(message, function (result) {
        if (result) {
            if (controlID != null) {
                var controlToClick = document.getElementById(controlID);
                if (controlToClick != null) {
                    confirmed = true;
                    controlToClick.click();
                    confirmed = false;
                }
            }
        }
    });

    return false;

    //return confirm('Are you sure?');
}


function OpenWindow(linktarget) {
    var Mleft = (screen.width / 2) - (760 / 2);
    var Mtop = (screen.height / 2) - (700 / 2);
    window.open(window.location.origin + '/' + linktarget, null, 'height=700,width=760,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'');
}

function ClickASPButton(s) {
    $("[id$='" + s + "']").click();
}

function ConvertToNumber(value) {
    return Number(value.replace(/[^0-9\.]+/g, ""));
}


function htmlDecode(input) {
    var e = document.createElement('div');
    e.innerHTML = input;
    return e.childNodes[0].nodeValue;
}
