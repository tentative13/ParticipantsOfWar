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
            $rootScope.table_loader = false;
            participantsService.getParticipants(filter, number)
                .done(function (data) {
                    $log.log('getParticipants recieved data:', data);
                    angular.forEach(data, function (item) {
                        var check = $.grep(self.Participants, function (f) { return f.guid == item.guid; });
                        if (check.length === 0)self.Participants.push(item);
                    });
                    $rootScope.table_loader = true;
                    $rootScope.$apply();
                })
                .fail(function () {
                    $rootScope.table_loader = true;
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
            $rootScope.table_loader = false;
            participantsService.getAllParticipants(filter)
                .done(function (data) {
                    $rootScope.table_loader = true;
                    $log.log('getAllParticipants recieved data:', data);
                    angular.forEach(data, function (item) {
                        var check = $.grep(self.Participants, function (f) { return f.guid == item.guid; });
                        if (check.length === 0)self.Participants.push(item);

                    });
                    $rootScope.$apply();
                })
                .fail(function () {
                    $rootScope.table_loader = true;
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
                self.ParticipantsTypes.push({ name: item["name"], value: item["value"] });
            });
        });

        self.UpdateCacheParticipants = function (newitem) {

            for (var i = 0; i < self.Participants.length; i++) {
                if (newitem.guid === self.Participants[i].guid) {
                    self.Participants[i] = newitem;
                }
            }
        };

        self.deleteParticipant = function (guid) {
            if (guid) {
                participantsService.deleteParticipant(guid, function () {
                    for (var i = 0; i < self.Participants.length; i++) {
                        if (guid === self.Participants[i].guid) {
                            self.Participants.splice(i, 1);
                            break;
                        }
                    }
                });
            }
        };

        self.AddToCacheParticipants = function (newitem) {
            var check = $.grep(self.Participants, function (f) { return f.guid == newitem.guid; });
            if (check.length === 0) {
                self.Participants.push(newitem);
            }
            else {
                self.UpdateCacheParticipants(newitem);
            }
        };

        return self;
    }]);
}());