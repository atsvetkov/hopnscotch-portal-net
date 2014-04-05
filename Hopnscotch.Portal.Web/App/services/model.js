define(['knockout'], function(ko) {
    var model = {
        configureMetadataStore: configureMetadataStore
    };

    return model;

    function configureMetadataStore(metadataStore) {
        metadataStore.registerEntityTypeCtor('User', null, userInitializer);
    }

    function userInitializer(user) {
        user.displayName = ko.computed(function () {
            return user.firstName() + ' ' + user.lastName();
        });
    }
});