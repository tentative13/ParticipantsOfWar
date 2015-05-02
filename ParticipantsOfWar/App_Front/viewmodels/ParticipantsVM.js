(function () {
    var app = angular.module('pow_app');

    app.factory('participantsVM', ['$rootScope', 'ParticipantsService', '$log', function ($rootScope, participantsService, $log) {

        var self = this;
        self.ParticipantsTypes = [];
        self.Participants = [];
        self.ParticipantsTypes.push({ name: "Все", value: 0 });
        self.TotalParticipants = 0;

        $rootScope.table_loader = false;

        self.GetPageParticipants = function (filter, number) {
            $log.info('calling getParticipants');
            participantsService.getParticipants(filter, number)
                .done(function (data) {
                    $log.log('getParticipants recieved data:', data);
                    angular.forEach(data, function (item) {
                        var check = $.grep(self.Participants, function (f) { return f.guid == item.guid; });
                        if (check.length === 0)self.Participants.push(item);
                    });
                    $rootScope.table_loader = true;
                    $rootScope.$apply();
            });
        };
        
        self.GetTotalFilteredParticipants = function (filter) {
            $log.info('calling getTotalFilteredParticipants');
            participantsService.getTotalFilteredParticipants(filter)
            .done(function (data) {
                $log.log('getTotalFilteredParticipants recieved data:', data);
                self.TotalParticipants = data;
                $rootScope.$apply();
            });
        };

        self.GetAllParticipants = function (filter) {
            $log.info('calling getAllParticipants');
            participantsService.getAllParticipants(filter)
                .done(function (data) {
                    $log.log('getAllParticipants recieved data:', data);
                    angular.forEach(data, function (item) {
                        var check = $.grep(self.Participants, function (f) { return f.guid == item.guid; });
                        if (check.length === 0)self.Participants.push(item);

                    });
                    $rootScope.$apply();
                });
        };

        self.SendGuidsCache = function () {
            if (self.Participants.length > 0) {
                var guids = [];
                angular.forEach(self.Participants, function (item) {
                    guids.push(item.guid);
                });
                $log.info('sending cached guids:', guids);
                participantsService.sendGuidsCache(guids);
            }
        }

        participantsService.getTypes(function (data) {
            $log.log('recieved types: ',data);
            angular.forEach(data, function (item) {
                self.ParticipantsTypes.push({ name: item["name"], value: item["priority"] });
            });
        });

        return self;
    }]);
}());