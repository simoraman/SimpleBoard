function taskViewModel() {
    var self = this;

    this.todoTasks = ko.observableArray([]);
    this.doingTasks = ko.observableArray([]);
    this.doneTasks = ko.observableArray([]);

    self.load = function () {
        $.ajax({
            url: "http://localhost:11188/tasks/",
            dataType: 'jsonp',
            success: function (result) {
                ko.utils.arrayPushAll(self.todoTasks, result);
                self.todoTasks.valueHasMutated();
            },
            error: function (request, textStatus, errorThrown) {
                alert(textStatus);
            }
        });
    }
};