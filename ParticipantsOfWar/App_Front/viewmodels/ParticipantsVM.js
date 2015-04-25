(function () {
    var app = angular.module('pow_app');

    app.factory('participantsVM', ['$rootScope', 'ParticipantsService', '$log', function ($rootScope, participantsService, $log) {

        var self = this;
        self.ParticipantsTypes = [];
        self.Participants = [];
        self.ParticipantsTypes.push({ name: "Все", value: 0 });
        self.TotalParticipants = 0;

        participantsService.getTypes(function (data) {
            var i = 1;
            angular.forEach(data, function (item) {
                self.ParticipantsTypes.push({ name: item["name"], value: i });
                i++;
            });
            
            participantsService.getAllParticipants(function (data) {
                angular.forEach(data, function (item) {

                    if (item.type === '') {
                        item.type_value = 0;
                    }
                    else {
                        var type = $.grep(self.ParticipantsTypes, function (f) { return f.name == item.type; });
                        item.type_value = type[0].value;
                    }
                    self.Participants.push(item);
                });
                self.TotalParticipants = self.Participants.length;
            });





        });

        self.paging = {
            pageSize: 10,
            getLastCount: function (filter, tablelength) {
                var lastLength = self.TotalParticipants - tablelength;
                return lastLength < 0 ? null : lastLength >= this.pageSize ? this.pageSize : lastLength;
            },
            showMore: function (filter, remains) {
            },
            showAll: function (filter) {
            }
        }


        return self;
    }]);
}());