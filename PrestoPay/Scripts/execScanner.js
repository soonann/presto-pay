var options;
var latestVal;
var timerOut;

function gotDevices(deviceInfos) {

    for (var i = 0; i !== deviceInfos.length; ++i) {
        var deviceInfo = deviceInfos[i];
        var newBtn = document.createElement('button');

        if (deviceInfo.kind === 'audioinput') {

            console.log(deviceInfo.deviceId);

        } else if (deviceInfo.kind === 'videoinput') {

            latestVal = deviceInfo.deviceId;
            (latestVal);
            newBtn.innerHTML = "btn" + i;
            newBtn.addEventListener('click', function () {

                if (window.stream) {
                    window.stream.getTracks().forEach(function (track) {
                        track.stop();
                    });
                }

                flip(deviceInfo.deviceId);

                timerOut = setTimeout(loadImageOnCanvas, 100)
            });

            document.getElementById('hi').innerHTML += deviceInfo.deviceId + "<br/>";
            document.body.appendChild(newBtn);

        } else {
            console.log('Found some other kind of source/device: ', deviceInfo);
        }
    }
}



var video = document.querySelector("#videoElement");

navigator.getUserMedia = navigator.getUserMedia || navigator.webkitGetUserMedia || navigator.mozGetUserMedia || navigator.msGetUserMedia || navigator.oGetUserMedia;

if (navigator.getUserMedia) {
    var gum = face => navigator.mediaDevices.getUserMedia({ video: { facingMode: { exact: face } } }).then(stream => (video.srcObject = stream))
        .catch(e => videoError(e));
    gum('environment');

}
else {
    document.getElementById("clientBody_Button_ScanQrCode").style.display = "none";
    document.getElementById("errorMessage").style.display = "block";
}



function handleVideo(stream) {
    //window.stream = stream;
    video.src = window.URL.createObjectURL(stream);
    //video.srcObject = stream;
}

function videoError(e) {
    // do something
   // alert(e);
    alert("Unable to start the QR Code scanner on your browser. Please try again after closing other applications using the camera or try again later !");

}


var gCanvas;
function initCanvas(w, h) {
    // set ID for plugin
    gCanvas = document.getElementById("qr-canvas");
    gCanvas.style.width = w + "px";
    gCanvas.style.height = h + "px";
    gCanvas.width = w;
    gCanvas.height = h;
    gCtx = gCanvas.getContext("2d");
    gCtx.clearRect(0, 0, w, h);
}
function isCanvasSupported() {
    var elem = document.createElement('canvas');
    return !!(elem.getContext && elem.getContext('2d'));
}
/*
function read(a) {
    var html = "<br>";
    if (a.indexOf("http://") === 0 || a.indexOf("https://") === 0)
        html += "<a target='_blank' href='" + a + "'>" + a + "</a><br>";
    html += "<b>" + htmlEntities(a) + "</b><br><br>";
    document.getElementById("result").innerHTML = html;
<div id="result"></div>
}
*/

function fireAjax(data) {
    var initialKey = data;
    $("#clientBody_HiddenField_UserScannedKey").val(initialKey);
    MasterPageForm.submit();

    /*
    $.ajax({

        url: 'PrestoQrPay.aspx/QrCodeValidity',
        type: 'POST',
        data: '{key: "' + data + '"}',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (data) {
            makePayment(initialKey, prompt("Please enter the amount you wish to transfer"));
        },
        error: function (data) {
            alert("Unidentified User ! Please try again !");
        }

    });
    */
}
/*
function makePayment(key, amount) {
    if (isNaN(amount))
        alert("Invalid amount !");
    $.ajax({

        url: 'PrestoQrPay.aspx/MakePayment',
        type: 'POST',
        data: '{key: "' + key + '", amount:' + amount + '}',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (data) {
            if (data.d.success)
                alert("Transaction successful !\nYour transaction number is: " + data.d.value);
            else
                alert("Transaction was unsuccessful !\n" + data.d.value + " !")
        },
        error: function (data) {
            alert("Error ! Please try again !");
        }

    });

}
*/
function loadImageOnCanvas() {
    try {
        gCanvas.getContext("2d").drawImage(video, 0, 0);
        try {
            qrcode.decode();
        }
        catch (e) {
            console.log(e);
            timerOut = setTimeout(loadImageOnCanvas, 100);
        };
    }
    catch (e) {
        console.log(e);
        timerOut = setTimeout(loadImageOnCanvas, 100);
    };
}

initCanvas(500, 500);

qrcode.callback = fireAjax;

function startScanning() {

    timerOut = setTimeout(loadImageOnCanvas, 100);
    document.getElementById("cover").style.display = "none";
    document.getElementById("text").style.display = "block";

}
function stopScanning() {
    var id = window.setTimeout(function () { }, 0);
    while (id--) {
        window.clearTimeout(id); 
    }
    document.getElementById("cover").style.display = "block";
    document.getElementById("text").style.display = "none";

}
