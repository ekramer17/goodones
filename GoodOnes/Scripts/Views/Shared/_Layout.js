var pushRight = new Menu({
    wrapper: '#o-wrapper',
    type: 'push-right',
    menuOpenerClass: '.c-button',
    maskId: '#c-mask'
});

var pushRightBtn = document.querySelector('#c-button--push-right');

pushRightBtn.addEventListener('click', function (e) {
    e.preventDefault;
    pushRight.open();
});

$(document).ready()
{
    flash();
}

/*
* Flash messaging
*/
function flash(severity, message, css, dismiss)
{
    var s = '';
    var m = typeof(message) == 'string' ? [message] : message;

    if (m != null) {

        $('.flash-msg').remove();

        for (var i = 0; i < m.length; ++i) {
            s += "<div role='alert' style='display:none' ";
            s += "class='alert alert-block alert-" + severity + " ";
            s += "flash-msg " + (css && css.length != 0 ? css : "") + "'>";

            if (dismiss !== false) {
                s += "<button type='button' aria-label='close' class='close'>";
                s += "<span aria-hidden='true'>&times;</span>";
                s += "</button>";
            }

            s += "<span>" + m[i] + "</span>";
            s += "</div>";
        }

        $('#flash-container').html(s);
    }

    var f = $('.flash-msg');
    if (f.length != 0) {
        f.show(200);
        f.find('button').on('click', function (e) {
            e.preventDefault();
            $(this).parent().hide(200);
        });
    }
}

/*
* This bit of code disables user scaling on iOS until the user tries to scale with pinch/zoom.
* http://stackoverflow.com/questions/2557801/how-do-i-reset-the-scale-zoom-of-a-web-app-on-an-orientation-change-on-the-iphon
*/
if (navigator.userAgent.match(/iPhone/i) || navigator.userAgent.match(/iPad/i)) {
    var viewportmeta = document.querySelector('meta[name="viewport"]');
    if (viewportmeta) {
        viewportmeta.content = 'width=device-width, minimum-scale=1.0, maximum-scale=1.0, initial-scale=1.0';
        document.addEventListener('gesturestart', function () {
        viewportmeta.content = 'width=device-width, minimum-scale=0.25, maximum-scale=10';
        }, false);
    }
}
