$(window).scroll(function () {
    if ($(window).width() > 319) {

        if ($(window).scrollTop() > 1) {
            $('#mainHeader').addClass('fixedHeader');


        } else {
            $('#mainHeader').removeClass('fixedHeader');


        }
    }
});