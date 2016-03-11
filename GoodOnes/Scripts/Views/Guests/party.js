$(document).ready(function () {

    $('#select-link').on('click', function (e) {
        e.preventDefault();
        select($('#party-id').val());
    })

    $("#dncalendar-container").dnCalendar({
        notes: notes,
        initial: initial,
        minDate: mindate,
        maxDate: maxdate,
        drawCallback: function() {
            var attr = [];
            var date = $('#party-date').val().split('-');

            attr.push("[data-year=" + date[0] + "]");
            attr.push("[data-date=" + parseInt(date[2]) + "]");
            attr.push("[data-month=" + parseInt(date[1]) + "]");
            var cell = $('.dncalendar-note' + attr.join(''));

            if (cell.length != 0 && !cell.hasClass('active'))
            {
                $('.dncalendar-note.active').removeClass('active');
                cell.addClass('active');
            }
        },
        dayClick: function(date) {

            var $this = $(this);

            var m = '' + (date.getMonth() + 1);
            var d = '' + date.getDate();
            var y = date.getFullYear();
            if (m.length < 2) m = '0' + m;
            if (d.length < 2) d = '0' + d;

            $.ajax({
                type: 'GET',
                url: geteventurl,
                data: { date: [y, m, d].join('-') },
                success: function (o) {
                    $('.event-title').text(o.Title);
                    $('.line1').text(o.Line1);
                    $('.line2').text(o.Line2);
                    $('.day-of-week').text(o.DayName);
                    $('#party-id').val(o.ID);
                    $('#party-date').val(o.Date);
                    $('.dncalendar-note.active').removeClass('active');
                    $this.addClass('active');
                },
                error: function(xhr, status, thrown) {
                    flash("danger", xhr.responseText, "row");
                }

            });
        }
    }).build();
});

function select(i)
{
    spinner(true);

    $.ajax({
        type: 'POST',
        url: pickpartyurl,
        data: { id: i },
        headers: { 'RequestVerificationToken': _afh },
        success: function () {
            location.href = nextpageurl + "?party=" + i
        },
        error: function (xhr, status, thrown) {
            spinner(false);
            flash("danger", xhr.responseText, "row");
        }
    });
}

function spinner(b)
{
    var $hide = $(b ? "#select-link span" : ".spinner-3balls");
    var $show = $(b ? ".spinner-3balls" : "#select-link span");
    $hide.css('display', 'none');
    $show.css('display', 'block');
}