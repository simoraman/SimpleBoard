function taskViewModel() {
    var self = this;

    self.todoTasks = ko.observableArray([]);
    self.doingTasks = ko.observableArray([]);
    self.doneTasks = ko.observableArray([]);

    self.url = "http://localhost:11188";

    self.load = function () {
//        $.ajax({
//            url: url + "/tasks/",
//            dataType: 'jsonp',
//            success: function (result) {
//                ko.utils.arrayPushAll(self.todoTasks, result);
//                self.todoTasks.valueHasMutated();
//            },
//            error: function (request, textStatus, errorThrown) {
//                alert(textStatus);
//            }

//        });
        self.loadByStatus("ToDo", self.todoTasks);
        self.loadByStatus("Doing", self.doingTasks);
        self.loadByStatus("Done", self.doneTasks);
    }

    self.loadByStatus = function (status, list) {
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