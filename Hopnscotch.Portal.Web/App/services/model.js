define(function() {
    var Lead = function(dto) {
        return mapToObservable(dto);
    };

    var Contact = function (dto) {
        return addLeadComputeds(mapToObservable(dto));
    };

    var model = {
        Lead: Lead,
        Contact: Contact
    };

    return model;

    function mapToObservable(dto) {
        var mapped = {};
        for (prop in dto) {
            if (dto.hasOwnProperty(prop)) {
                mapped[prop] = ko.observable(dto[prop]);
            }
        }

        return mapped;
    }

    function addLeadComputeds(entity) {
        // add any necessary computed observables here

        return entity;
    }

});