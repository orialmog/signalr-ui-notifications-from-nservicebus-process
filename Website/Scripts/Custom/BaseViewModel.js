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


//Common view model base
var Base = function () {

    this.id = ''; //id of view model
    this.state = ko.observable(''); // string as current state which relates to template id.

    //check if can transition
    this.onTransition = function (oldState, newState, success) {
        success(); // just go to next state
    };

    //callback for after transtion effected.
    this.afterTransition = function (oldState, newState) {

    };

    //function to transtion states for this view model
    this.transitionTo = function (newstate) {
        var that = this;
        that.onTransition(
            that.state(),
            newstate,
            function () {
                that.state(newstate);
                that.afterTransition(that.state(), newstate);
            });
    };

    this.ajax = function (request) { 
        var data = ko.toJSON(request.data || this);
        $.ajax({
            type: request.type,
            url: request.url,
            data: data,
            accept: request.accept !== null ? request.accept : 'application/json',
            contentType: request.contentType !== null ? request.contentType : 'application/json',
            success: request.success,
            failure: request.failure
        });

    }

}

