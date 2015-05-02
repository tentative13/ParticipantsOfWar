(function () {
    var app = angular.module('pow_app');
    app.filter('participantsTableFilter', function () {
        /* array is first argument, each addiitonal argument is prefixed by a ":" in filter markup*/
        return function (dataArray, searchTerm) {
            if (!dataArray) return;
            /* when term is cleared, return full array*/
            if (!searchTerm) {
                return dataArray
            } else {
                /* otherwise filter the array */
                return dataArray.filter(function (item) {
                    var check_firstname = true;
                    var check_surname = true;
                    var check_middlename = true;
                    var check_type = true;
                    var check_birthday = true;
                    

                    for (var member in searchTerm) {
                        if (searchTerm[member] != '' && member == 'firstname') check_firstname = false;
                        if (searchTerm[member] != '' && member == 'middlename') check_middlename = false;
                        if (searchTerm[member] != '' && member == 'surname') check_surname = false;
                        if (typeof searchTerm[member] != 'undefined' && member == 'birthday') check_birthday = false;
                    }

                    if (typeof searchTerm.firstname != 'undefined')
                    {
                        if (item.firstname.toLowerCase().substring(0, searchTerm.firstname.toString().length) == searchTerm.firstname.toString().toLowerCase()) check_firstname = true;
                    }
                    if (typeof searchTerm.middlename != 'undefined')
                    {
                        if (item.middlename.toLowerCase().substring(0, searchTerm.middlename.toString().length) == searchTerm.middlename.toString().toLowerCase()) check_middlename = true;
                    }
                    if (typeof searchTerm.surname != 'undefined')
                    {
                        if (item.surname.toLowerCase().substring(0, searchTerm.surname.toString().length) == searchTerm.surname.toString().toLowerCase()) check_surname = true;
                    }
                    if (searchTerm.ParticipantsTypes != 0) {
                        check_type = false;
                        if (searchTerm.ParticipantsTypes == item.type_value) check_type = true;
                    }
                    if (typeof searchTerm.birthday != 'undefined')// && searchTerm.MessageBody != '')
                    {
                        var d = new Date(item.birthday);
                        var idate = new Date(d.getFullYear(), d.getMonth(), d.getDate());
                        var d2 = new Date(searchTerm.birthday);
                        var filterdate = new Date(d2.getFullYear(), d2.getMonth(), d2.getDate());
                        if (idate.getTime() == filterdate.getTime()) check_birthday = true;
                    }

                    return check_firstname && check_surname && check_type && check_birthday && check_middlename;
                });
            }
        }
    });


}());
