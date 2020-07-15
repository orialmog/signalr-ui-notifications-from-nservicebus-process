function guid() {

    function s4() {
        return Math.floor((1 + Math.random()) * 0x10000)
            .toString(16)
            .substring(1);
    }

    return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
        s4() + '-' + s4() + s4() + s4();
}

ko.bindingHandlers.dynaTemplate = {
    init: ko.bindingHandlers.template.init,
    update: function (element, valueAccessor, allBindings, data, context) {
        //ensure that we have a dependency on the id
        var options = ko.unwrap(valueAccessor()),
            id = options && typeof options === "object" ? ko.unwrap(options.id) : id,
            $el = $(element);

        if ($el.html()) {
            $el.hide();
            ko.bindingHandlers.template.update(element, valueAccessor, allBindings, data, context);
            $el.fadeIn(250);
        }
        else {
            ko.bindingHandlers.template.update(element, valueAccessor, allBindings, data, context);
        }
    }
};


ko.bindingHandlers.fadeIn = {
    update: function (element, valueAccessor) {
        var value = ko.utils.unwrapObservable(valueAccessor());

        if (value === true) {
            $(element).animate({

                opacity: 1
            }, {
                duration: 100,
                queue: false
            });
        } else {
            $(element).animate({
                opacity: 0
            }, {
                duration: 100,
                queue: false
            });
        }
    }
};
 
