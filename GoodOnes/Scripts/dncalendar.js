/* ========================================================================
 * DNCalendar - v1.0
 * https://github.com/black-lotus/dnCalendar
 * ========================================================================
 * Copyright 2015 WPIC, Romdoni Agung Purbayanto
 *
 * ========================================================================
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * ========================================================================
 */

(function ( $ ) {

    $.fn.dnCalendar = function( options ) {
        var self = $(this);
        var settings = {};

        // current date
        var currDate = new Date();
        // custom default date
        var defDate = null;
        // today date
        var todayDate = new Date();

        /*
        * get total weeks in month
        *
        * @param int
        * @param int, range 1..12
        */
        var weekCount = function(year, month_number) {
            var firstOfMonth = new Date(year, month_number-1, 1);
            var lastOfMonth = new Date(year, month_number, 0);

            var used = firstOfMonth.getDay() + lastOfMonth.getDate();

            return Math.ceil(used / 7);
        }

        /*
        * draw calendar and give call back when date selected
        */
        var draw = function() {
            var m = currDate.getMonth(); // get month
            var d = currDate.getDate(); // get date of month
            var y = currDate.getFullYear(); // get full year

            // get month name
            var headerMonth = settings.monthNames[m];
            if (settings.monthUseShortName == true) {
                headerMonth = settings.monthNamesShort[m];
            }

            // create header label
            var headerGroup = $("<div id='dncalendar-header' class='dncalendar-header'></div>");

            // determine prev link as false
            var prevInactive = false;

            // set prev link as true when minDate is exist and current date is less than or equal minDate
            var minDate = null;
            if (typeof settings.minDate !== 'undefined') {
                var minDateArr = settings.minDate.split('-');
                minDate = new Date(minDateArr[0], minDateArr[1] - 1, minDateArr[2]);
                if (minDate.getFullYear() >= y) {
                    if (minDate.getMonth() >= m) {
                        prevInactive = true;
                    }
                }
            }

            // determine prev link as false
            var nextInactive = false;

            // set next link as true when maxDate is exist and current date is greater than or equal maxDate
            var maxDate = null;
            if (typeof settings.maxDate !== 'undefined') {
                var maxDateArr = settings.maxDate.split('-');
                maxDate = new Date(maxDateArr[0], maxDateArr[1] - 1, maxDateArr[2]);
                      
                if (maxDate.getFullYear() <= y) {
                    if (maxDate.getMonth() <= m) {
                        nextInactive = true;
                    }
                }
            }

            // links (prev, current, next)
            var ul = $("<ul></ul>");
            ul.append("<li id='dncalendar-prev-month' class='month prev " + (prevInactive ? "inactive" : "") + "'><span></span></li>")
            ul.append("<li>" + headerMonth + " " + y + "</li>");
            ul.append("<li id='dncalendar-next-month' class='month next" + (nextInactive ? "inactive" : "") + "'><span></span></li>")
            headerGroup.append(ul);

            var bodyGroup = $("<div id='dncalendar-body' class='dncalendar-body'></div>");
            var tableGroup = $("<table></table>");

            var weekName = settings.dayNames;
            if (settings.dayUseShortName == true) {
                weekName = settings.dayNamesShort;
            }

            var tableHeadGroup = $("<thead></thead>");
            var tableHeadRowGroup = $("<tr></tr>");
            var weekNameLength = weekName.length;
            for (var i = 0; i < weekNameLength; i++) {
                tableHeadRowGroup.append("<td>"+ weekName[i] +"</td>");
            }
            tableHeadGroup.append(tableHeadRowGroup);

            var tableBodyGroup = $("<tbody></tbody>");
            var totalWeeks = weekCount(y, m + 1);
            var totalDaysInWeeks = 7;
            var startDate = 1;

            var firstDayOfMonth = new Date(y, m, 1); // get first day of month
            var lastDayOfMonth = new Date(y, m + 1, 0); // get last day of month
              
            var lastDateOfPrevMonth = new Date(y, m, 0); // get last day of previous month
            var prevDate = lastDateOfPrevMonth.getDate() - firstDayOfMonth.getDay() + 1;

            var firstDateOfNextMonth = new Date(y, m + 1, 1); // get fist day of next month
            var nextDate = firstDateOfNextMonth.getDate();

            var limitMinDate = 0;
            if (minDate != null) {
                limitMinDate = minDate.getDate();
            }

            var limitMaxDate = 0;
            if (maxDate != null) {
                limitMaxDate = maxDate.getDate();
            }

            var todayTitle = 'today';
            var defaultDateTitle = 'default date';
            if (typeof settings.dataTitles !== 'undefined') {
                if (typeof settings.dataTitles.defaultDate !== 'undefined') {
                    defaultDateTitle = settings.dataTitles.defaultDate;
                }  

                if (typeof settings.dataTitles.today !== 'undefined') {
                    todayTitle = settings.dataTitles.today;
                } 
            }

            for (var i = 0; i < totalWeeks; i++) {
                var tableBodyRowGroup = $("<tr></tr>");
                for (var j = 0; j < totalDaysInWeeks; j++) {
                    if ( (i != 0 && i != totalWeeks - 1) || (i == 0 && j >= firstDayOfMonth.getDay()) || (i == totalWeeks - 1 && j <= lastDayOfMonth.getDay())) {

                        var colDate = $("<td>" + startDate + "</td>");

                        if (typeof settings.notes !== 'undefined') {

                            var d = new Date(y, m, startDate);
                            d.setHours(0, 0, 0, 0);

                            if (dateIsNotes(d) && d > new Date()) {     // future date and has note
                                var klass = "dncalendar-note";
                                if (settings.initial == y + "-" + ("00" + (m + 1)).slice(-2) + "-" + ("00" + startDate).slice(-2))
                                    klass += " active";
                                colDate = $("<td><div class='" + klass + "' data-date='" + startDate
                                    + "' data-month='" + (m + 1) + "' data-year='" + y + "'>" + startDate + "</div></td>");
                            }

                        }

                        tableBodyRowGroup.append(colDate);

                        startDate++;

                    } else {

                        tableBodyRowGroup.append("<td></td>");

                    }
                          
                } // end for (j)

                tableBodyGroup.append(tableBodyRowGroup);

            } // end for (i)

            tableGroup.append(tableHeadGroup);
            tableGroup.append(tableBodyGroup);
            bodyGroup.append(tableGroup);

            self.html("");
            self.append(headerGroup);
            self.append(bodyGroup);

            settings.drawCallback.call(this);
        }

        var dateIsNotes = function(date) {
            var notesLength = settings.notes.length;
            for (var i = 0; i < notesLength; i++) {
                var dateNote = settings.notes[i].date.split('-');
                var nDate = new Date(dateNote[0], dateNote[1] - 1, dateNote[2]);

                if ( nDate.getFullYear() == date.getFullYear() && nDate.getMonth() == date.getMonth() && nDate.getDate() == date.getDate() ) {
                    return true;
                }
            }

            return false;
        }

        var nextMonth = function() {
            var firstDateOfNextMonth = new Date(currDate.getFullYear(), currDate.getMonth() + 1, 1); // get first day of next month
            var date = firstDateOfNextMonth.getDate();
            var month = firstDateOfNextMonth.getMonth();
            var year = firstDateOfNextMonth.getFullYear();

            currDate = new Date(year, month, date);
            settings.initial = '';
            draw();
        }

        var prevMonth = function() {
            var firstDateOfPrevMonth = new Date(currDate.getFullYear(), currDate.getMonth() - 1, 1); // get first day of previous month
            var date = firstDateOfPrevMonth.getDate();
            var month = firstDateOfPrevMonth.getMonth();
            var year = firstDateOfPrevMonth.getFullYear();

            currDate = new Date(year, month, date);
            settings.initial = '';
            draw();
        }

        var triggerAction = function() {

            $('body').on('click', '.dncalendar-note', function() {
                var selectedDate = $(this).data('date');
                var selectedMonth = $(this).data('month');
                var selectedYear = $(this).data('year');

                settings.dayClick.call(this, new Date(selectedYear, selectedMonth - 1, selectedDate), self);

            });

            $('body').on('click', '#dncalendar-prev-month', function() {
                prevMonth();
            });

            $('body').on('click', '#dncalendar-next-month', function() {
                nextMonth();
            });
        }

        return {
            build: function() {
                settings = $.extend( {}, $.fn.dnCalendar.defaults, options );

                // replace with defaultDate when exist
                if (typeof settings.defaultDate !== 'undefined') {
                    var defaultDateArr = settings.defaultDate.split('-');
                    currDate = new Date(defaultDateArr[0], defaultDateArr[1] - 1, defaultDateArr[2]);
                    defDate = currDate;
                }

                draw();
                triggerAction();
            },
            update: function(options) {
                settings = $.extend(settings, options);

                // replace with defaultDate when exist
                if (typeof settings.defaultDate !== 'undefined') {
                    var defaultDateArr = settings.defaultDate.split('-');
                    currDate = new Date(defaultDateArr[0], defaultDateArr[1] - 1, defaultDateArr[2]);
                    defDate = currDate;
                }

                draw();
            }
        }
    }

    // plugin defaults 
    $.fn.dnCalendar.defaults = { 
        monthNames: [ "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" ], 
        monthNamesShort: [ 'Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec' ],
        dayNames: [ 'Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'],
        dayNamesShort: [ 'S', 'M', 'T', 'W', 'T', 'F', 'S' ],
        dayUseShortName: true,
        monthUseShortName: false,
        drawCallback: function() {},
        dayClick: function(date, view) {},
        showNotes: false,
        initial: ''
    };

} ( jQuery ));