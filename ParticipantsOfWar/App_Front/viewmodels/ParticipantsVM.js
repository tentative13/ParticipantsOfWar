(function () {
    var app = angular.module('pow_app');

    app.factory('participantsVM', ['$rootScope', 'ParticipantsService', '$log', function ($rootScope, participantsService, $log) {

        var self = this;
        self.ParticipantsTypes = [];
        self.Participants = [];
        self.ParticipantsTypes.push({ name: "Все", value: 0 });
        self.TotalParticipants = 0;

        $rootScope.table_loader = false;
        participantsService.getTypes(function (data) {
            //var i = 1;
            angular.forEach(data, function (item) {
                self.ParticipantsTypes.push({ name: item["name"], value: item["priority"] });
             //   i++;
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

                $rootScope.table_loader = true;
                $rootScope.$emit('ParticipantsLoaded');
            });

        });

        return self;
    }]);
}());