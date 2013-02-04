function taskViewModel() {
    var self = this;

    self.todoTasks = ko.observableArray([]);
    self.doingTasks = ko.observableArray([]);
    self.doneTasks = ko.observableArray([]);

    self.url = "http://localhost:11188";

    self.load = function () {
        loadByStatus("ToDo", self.todoTasks);
        loadByStatus("Doing", self.doingTasks);
        loadByStatus("Done", self.doneTasks);
    }
    
    loadByStatus = function (status, list) {
        $.ajax({
            url: self.url + "/tasks/status/"+status,
            dataType: 'jsonp',
            success: function (result) {
                ko.utils.arrayPushAll(list, result);
                list.valueHasMutated();
            },
            error: function (request, textStatus, errorThrown) {
                alert(textStatus);
            }
        });
    }
};