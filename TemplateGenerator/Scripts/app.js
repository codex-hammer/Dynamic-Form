angular.uppercase = function (text) {
    return text.toUpperCase();
}
angular.lowercase = function (text) {
    return text.toLowerCase();
}

var app = angular.module("app", ['ui.grid'])

app.controller('AppCtrl', ['$scope', '$http', '$window', function ($scope, $http, $window) {



   // $scope.testvar = [{ "loginId": "PrashantM", "name": "Prashant More", "status": "True", "location": "Mumbai", "counselorgpn": "IN010G00068", "createdon": "12-03-2018 12:11:00", "createdby": "IN010B03536" }, { "loginId": "SushilY", "name": "Sushil Yashwantrao", "status": "True", "location": "Mumbai", "counselorgpn": "IN010G00068", "createdon": "12-03-2018 12:15:00", "createdby": "IN010B03536" }, { "loginId": "test1", "name": "test1", "status": "True", "location": "Mumbai", "counselorgpn": "IN010J06567", "createdon": "08-06-2018 15:12:00", "createdby": "IN010J43706" }]




    $scope.optionresponse
    $scope.stdFormData = {};
    $scope.responseGet = {};
    $scope.rawTemplate = [];
    $scope.resultresponse = {};
    $scope.header = [];
    finalTemplate = [];
    keeptrack = [];
    method = "";
    keeptrack.push(9999);
    resultapi = "";
    $scope.urlFormData = {};
    pageid = "3";
    exiturl = "";
    url = "http://localhost:61064/api/Template/show/" + pageid;
         $http.get(url).then(function successCallback(response) {
            responseGet = response;
            rawTemplate = responseGet.data.ft_detail;
            header = responseGet.data.ft_head;
            n = rawTemplate.length;
            console.log(n);
            resultapi = header.apipath;
            method = header.methodname;
            temp = {};
            temp.type = "button";
            temp.model = "exitbtn";
            temp.label = "Exit";
            temp.name = -1;
            temp.callback = "exit()";
            finalTemplate.push(temp);

            $scope.exit = function () {
                var host = $window.location.host;
                var landingUrl = header.exiturl;
               // alert(landingUrl);
                $window.location.href = landingUrl;
            };


            temp = {};
            temp.type = "submit";
            temp.model = "result";
            temp.label = "Get Result";
            temp.name = n+5000;
            temp.callback = "getresult(stdFormData)";
            finalTemplate.push(temp);
            temp = {};
            temp.type = "reset";
            temp.model = "reset";
            temp.label = "Reset";
            temp.name = n + 4999;
            finalTemplate.push(temp);
            for (i = 0; i < n; i++) {
                // obj = {};
                x = rawTemplate[i];

                if (x.IsDateRange == "1") {
                    obj1 = {}; obj2 = {};
                    obj1.type = "date";
                    obj1.model = x.DBColumnName + "from$$";
                    obj1.label = x.DisplayColumnName+" From";
                    obj1.placeholder = "From Date";
                    obj1.name = parseInt(x.SequenceId);
                    //obj1.style = "width:30%;";
                    if (x.IsHidden == "1") { obj1.style = "{visibility:hidden}"; }

                    obj2.type = "date";
                    obj2.model = x.DBColumnName + "to$$";
                    obj2.label = x.DisplayColumnName + " To";
                    obj2.placeholder = "To Date";
                   // obj2.style = "width:30%;";
                    obj2.name = parseInt(x.SequenceId);
                    if (x.IsHidden == "1") { obj2.style = "{visibility:hidden}"; }

                    finalTemplate.push(obj1);
                    finalTemplate.push(obj2);
                    keeptrack.push(1);
                    keeptrack.push(1);

                }
                else if (x.IsSearchValue == "1") {
                    obj = {};
                    obj.type = "select";
                    obj.model = x.DBColumnName;
                    obj.label = x.DisplayColumnName;
                    obj.name = parseInt(x.SequenceId);
                    obj.empty = "nothing selected"
                    spmethod = x.Method;
                    searchquery = {};
                    searchquery.methodname = spmethod;
                    obj.options = []
                    for (j = 0; j < x.options.length; j++) {
                        temporary = {};
                        temporary.value = x.options[j].fieldvalue;
                        temporary.label = x.options[j].fieldtext;
                        obj.options.push(temporary);
                    }
                    
                    if (x.IsHidden == "1") { obj.style = "{visibility:hidden}"; }

                    finalTemplate.push(obj);
                    keeptrack.push(2);
                }
                else if (x.IsYesNo == "1") {
                    obj1 = {}; obj2 = {};
                    obj1.type = "radio";
                    obj1.values = ["YES","NO"];
                    obj1.model = x.DBColumnName;
                    obj1.label = x.DisplayColumnName;
                    //obj1.placeholder = "yes";
                    obj1.name = parseInt(x.SequenceId);
                    if (x.IsHidden == "1") { obj1.style = "{visibility:hidden}"; }

                    finalTemplate.push(obj1);
                    keeptrack.push(3);
                }
                else {
                    obj = {};
                    obj.type = "text";
                    obj.model = x.DBColumnName;
                    obj.label = x.DisplayColumnName;
                    obj.name = parseInt(x.SequenceId);
                    if (x.IsHidden == "1") { obj.style = "{visibility:hidden}"; }
                    finalTemplate.push(obj);
                    keeptrack.push(-1);
                }
            }
            keeptrack.push(9999);
            finalTemplate.sort(function (a, b) { return a.name - b.name })
            $scope.stdFormTemplate = finalTemplate;
            console.log(finalTemplate);
            $scope.getresult($scope.stdFormData, $scope.stdFormData);
         });

         $scope.getresult = function (stdFormData, resultresponse) {
             //console.log(stdFormData);
             gpn = "IN000000";
             var from, to;
             valid = 1;
             datecount = 0;
             count = 0;
             query = "";
             for (var property in stdFormData) {
                 if (property && stdFormData[property]) {
                     var l = property.toString().length;
                     var prop = property.toString();
                     if (prop.substr(l - 6) == "from$$") {
                         stdFormData[property] = moment(stdFormData[property]).format("YYYY-MM-DD HH:mm:ss");
                         if (count == 0) {
                             query = query + prop.slice(0, -6) + " >= '" + stdFormData[property]+"'";
                             datecount++;
                             count++;
                             from = stdFormData[property];
                             }
                         else {
                             query = query + " and " + prop.slice(0, -6) + " >= '" + stdFormData[property] + "'";
                             from = stdFormData[property];
                             datecount++;
                         }
                      }
                     else if (prop.substr(l - 4) == "to$$") {
                         stdFormData[property] = moment(stdFormData[property]).format("YYYY-MM-DD HH:mm:ss");
                         if (count == 0) {
                             query = query + prop.slice(0, -4) + " <= '" + stdFormData[property] + "'";
                             count++;
                             to = stdFormData[property];
                         }
                         else {
                             query = query + " and " + prop.slice(0, -4) + " <= '" + stdFormData[property] + "'";
                             to = stdFormData[property];
                             datecount++;
                         }
                     }
                     else {
                         if (count == 0) {
                             query = query + prop + " = '" + stdFormData[property] + "'";
                             count++;
                              }
                         else {
                             query = query + " and " + prop + " = '" + stdFormData[property] + "'";
                         }
                     }
                 }
                 
             }
             if (query != "") {
                 query = "where " + query;
             }
             console.log(query);
             url = resultapi;
             data = {};
             data.gpn = gpn;
             data.method = method;
             data.where_statement = query;

             if (from != null && to != null && datecount==2 && from > to) {
                 alert("From Date must be less than to date");
             }
             else {
             var config = {
                 headers: {
                     'Content-Type': 'application/json'
                 }
             }
                 $http.post(url, data, config)
                   .then(
                       function (response) {
                           $scope.resultresponse = response.data;
                           console.log(resultresponse);
                       },
                       function (response) {
                           console.log(response);
                       }
                    );
             }
         }



}]);


 app.filter('pretty', function() {
    return function (input) {
      var temp;
      try {
        temp = angular.fromJson(input);
      }
      catch (e) {
        temp = input;
      }
      
      return angular.toJson(temp, true);
    };
  });
