var enabled = true;
var checkout = null;
var tokenreceived = false;

$(document).ready(function () {

    checkout = StripeCheckout.configure({
        key: publickey,
        locale: 'auto',
        token: function (token) {
            tokenreceived = true;
            submit(token.email, token.id);
        },
        opened: function() {
            tokenreceived = false;
        },
        closed: function() {
            enable(!tokenreceived);
        }
    });

    $('.o-choice .text').on('click', function () {
        if (enabled) {
            enable(false);
            var o = $(this).closest('.o-choice');
            if (!o.hasClass('selected')) {
                o.parent().find('.o-choice').removeClass('selected');
                o.addClass('selected');
            }
            setTimeout(moveOff, 500);
        }
    })

    $('#guest-name').on('click', function () {
        if (enabled) {
            enable(false);
            $('#name-form').validator('validate');
            var $n1 = $('#first-name');
            var $n2 = $('#last-name');
            if ($.trim($n1.val()) == '')
            {
                $n1.focus();
                enable(true);
            }
            else if ($.trim($n2.val()) == '')
            {
                $n2.focus();
                enable(true);
            }
            else
            {
                setTimeout(moveOff, 200);
            }
        }
    });

    $('#checkout').on('click', function (e) {
        if (enabled)
        {
            enable(false);

            checkout.open({
                name: 'GoodOnes',
                description: 'One Guest Party Pass',
                amount: 2000
            });
        }
        e.preventDefault();
    });


    $('#rules').on('click', function (e) {
        location.href = rulesurl;
    });
    
    $(window).on('popstate', function () {
        checkout.close();
    });
});

function moveOff() {
    var b1 = $('.box1:not(.off):first');
    var b2 = $('.box2');
    var b3 = $('.box3:first');
    if (b1.length != 0) b1.addClass('off');
    if (b2.length != 0) b2.addClass('box1').removeClass('box2');
    if (b3.length != 0) b3.addClass('box2').removeClass('box3');

    var $i = b2.find('input:not(input[type=hidden])');
    if ($i.length != 0) $i[0].focus();

    setTimeout(function () { enable(true);}, 500);
}

function moveOn() {
    var bo = $('.box1.off:last');
    var b1 = $('.box1:not(.off)');
    var b2 = $('.box2');
    if (bo.length != 0) bo.removeClass('off');
    if (b1.length != 0) b1.addClass('box2').removeClass('box1');
    if (b2.length != 0) b2.addClass('box3').removeClass('box2');

    var $i = bo.find('input:not(input[type=hidden])');
    if ($i.length != 0) $i[0].focus();

    setTimeout(function () { enable(true); }, 500);
}

function submit(email, token)
{
    var answers = [];

    $('.question-box').each(function (i, e) {
        var q = $(e);
        var o = {};

        o.q = q.find('.question-id').val();

        if (q.find('.o-choice').length != 0)
        {
            var n = q.find('.o-choice.selected');
            o.a = $.trim(n.find('.text').text());
        }

        answers.push(o);
    })

    $.ajax({
        type: 'POST',
        url: surveyurl,
        data: {
            answers: JSON.stringify(answers),
            firstName: $.trim($('#first-name').val()),
            lastName: $.trim($('#last-name').val()),
            party: partyid,
            token: token,
            email: email
        },
        headers: {
            'RequestVerificationToken': _afh
        },
        success: function () {
            moveOff();
        },
        error: function (xhr, status, thrown) {
            enable(true);
            flash("danger", xhr.responseText, "row");
        }
    });
}

function enable(b)
{
    if (enabled != b) {

        enabled = b;

        if ($('#checkout').parent().hasClass('box1')) {
            var $show = $(b ? "#checkout span" : ".spinner-3balls");
            var $hide = $(b ? ".spinner-3balls" : "#checkout span");
            $hide.css('display', 'none');
            $show.css('display', 'block');
        }

    }
}
