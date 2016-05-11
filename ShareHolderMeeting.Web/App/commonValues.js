/// <reference path="D:\00-OnlineInsurance\ShareHolderMeeting\ShareHolderMeeting.Web\Scripts/angular.js" />

angular.module('commonValues', [])
       .value('statusOptions',
        [
            { id: 0, name: 'Absent' },
            { id: 1, name: 'Attended' },
            { id: 2, name: 'Delegated' }
        ]
        );
