(function ($) {
    function compareDates(startDate, endDate, format) {
        var temp, dateStart, dateEnd;
        try {
            dateStart = $.datepicker.parseDate(format, startDate);
            dateEnd = $.datepicker.parseDate(format, endDate);
            if (dateEnd < dateStart) {
                temp = startDate;
                startDate = endDate;
                endDate = temp;
            }
        } catch (ex) { }
        return { start: startDate, end: endDate };
    }

    $.fn.dateRangePicker = function (options) {
        options = $.extend({
            "changeMonth": false,
            "changeYear": false,
            "numberOfMonths": 2,
            "rangeSeparator": " - ",
            "useHiddenAltFields": false
        }, options || {});

        var myDateRangeTarget = $(this);
        var onSelect = options.onSelect || $.noop;
        var onClose = options.onClose || $.noop;
        var beforeShow = options.beforeShow || $.noop;
        var beforeShowDay = options.beforeShowDay;
        var lastDateRange;

        function storePreviousDateRange(dateText, dateFormat) {
            var start, end;
            dateText = dateText.split(options.rangeSeparator);
            if (dateText.length > 0) {
                start = $.datepicker.parseDate(dateFormat, dateText[0]);
                if (dateText.length > 1) {
                    end = $.datepicker.parseDate(dateFormat, dateText[1]);
                }
                lastDateRange = { start: start, end: end };
            } else {
                lastDateRange = null;
            }
        }

        options.beforeShow = function (input, inst) {
            var dateFormat = myDateRangeTarget.datepicker("option", "dateFormat");
            storePreviousDateRange($(input).val(), dateFormat);
            beforeShow.apply(myDateRangeTarget, arguments);
        };

        options.beforeShowDay = function (date) {
            var out = [true, ""], extraOut;
            if (lastDateRange && lastDateRange.start <= date) {
                if (lastDateRange.end && date <= lastDateRange.end) {
                    out[1] = "ui-datepicker-range";
                }
            }

            if (beforeShowDay) {
                extraOut = beforeShowDay.apply(myDateRangeTarget, arguments);
                out[0] = out[0] && extraOut[0];
                out[1] = out[1] + " " + extraOut[1];
                out[2] = extraOut[2];
            }
            return out;
        };

        options.onSelect = function (dateText, inst) {
            var textStart;
            if (!inst.rangeStart) {
                inst.inline = true;
                inst.rangeStart = dateText;
            } else {
                inst.inline = false;
                textStart = inst.rangeStart;
                if (textStart !== dateText) {
                    var dateFormat = myDateRangeTarget.datepicker("option", "dateFormat");
                    var dateRange = compareDates(textStart, dateText, dateFormat);
                    myDateRangeTarget.val(dateRange.start + options.rangeSeparator + dateRange.end);
                    inst.rangeStart = null;
                    if (options.useHiddenAltFields) {
                        var myToField = myDateRangeTarget.attr("data-to-field");
                        var myFromField = myDateRangeTarget.attr("data-from-field");
                        $("#" + myFromField).val(dateRange.start);
                        $("#" + myToField).val(dateRange.end);
                    }
                }
            }
            onSelect.apply(myDateRangeTarget, arguments);
        };

        //window.addEventListener("scroll", function () {
        //    if (window.scrollY >= 70) {
        //        document.querySelector("nav").style.backgroundColor = "white"
        //        $("nav>*").addClass("active")
        //        $("nav a").addClass("active")
        //    }
        //    else {
        //        document.querySelector("nav").style.backgroundColor = ""
        //        $("nav>*").removeClass("active")
        //        $("nav a").removeClass("active")
        //    }
        //})

        options.onClose = function (dateText, inst) {
            inst.rangeStart = null;
            inst.inline = false;
            onClose.apply(myDateRangeTarget, arguments);
        };

        return this.each(function () {
            if (myDateRangeTarget.is("input")) {
                myDateRangeTarget.datepicker(options);
            }
            myDateRangeTarget.wrap("<div class=\"dateRangeWrapper\"></div>");
        });
    };
}(jQuery));

$(document).ready(function () {
    $("#txtDateRange").dateRangePicker({
        showOn: "focus",
        rangeSeparator: " to ",
        dateFormat: "yy-mm-dd",
        useHiddenAltFields: true,
        constrainInput: true
    });
});



$(".lang").click(function () {
    $(".setting").fadeToggle()
})



window.addEventListener("scroll", function () {
    if (this.window.scrollY >= 320) {
        $(".ticket table tr").css("animation-name", "anime")
    }
})



$.get('https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/azerbaijan/today?unitGroup=metric&key=CR9C9C2CJYET5ET947TX2NUSU', (res) => {
    console.log(res)
    console.log(res.days[0].temp)
    $(".weather span").text(res.days[0].temp)
})
setInterval(() => {
    const date = new Date();
    console.log(date.getHours() + ' : ' + date.getMinutes())
    $(".hours").text(date.getHours() + ' : ' + date.getMinutes())
}, 1000)


