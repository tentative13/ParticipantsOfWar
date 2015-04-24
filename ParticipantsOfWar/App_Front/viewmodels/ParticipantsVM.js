(function () {
    var app = angular.module('pow_app');

    app.factory('participantsVM', ['$rootScope', 'ParticipantsService', '$log', function ($rootScope, participantsService, $log) {

        var self = this;
        self.ParticipantsTypes = [];
        self.Participants = [];

        participantsService.getTypes(function (data) {
            angular.forEach(data, function (item) {
                self.ParticipantsTypes.push({ name: item["name"] });
            });
        });

        participantsService.getAllParticipants(function (data) {
            angular.forEach(data, function (item) {
                self.Participants.push(item);
            });
        });


        return self;
    }]);
}());