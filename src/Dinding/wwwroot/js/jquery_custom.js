/*
    Project Name : Directory Listing HTML
    Author Company : Viaviwebtech	
    Author Website : http://www.viaviweb.com	
*/
"use strict";

function InitApp() {

    $('#vfx_loader_block').fadeOut("slow");

    if ($(".vfx-coutter-item").length > 0) {
        $('.vfx-coutter-item').counterUp({
            delay: 30,
            time: 1800
        });
    }

    $(document).on('click', function () {
        "use strict";
        $(window).on('scroll', function () {
            if ($(this).scrollTop() > 100) {
                $('.scrollup').fadeIn();
            } else {
                $('.scrollup').fadeOut();
            }
        });
        $('.scrollup').on('click', function () {
            $("html, body").animate({ scrollTop: 0 }, 600);
            return false;
        });
    });
}