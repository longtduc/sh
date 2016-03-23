function Pager(tableName, itemsPerPage) {

    var tableName = tableName;

    var itemsPerPage = itemsPerPage;

    var currentPage = 1;

    var pages = 0;

    var inited = false;

    this.showRecords = function (from, to) {

        var rows = document.getElementById(tableName).rows;

        // i starts from 1 to skip table header row

        for (var i = 1; i < rows.length; i++) {

            if (i < from || i > to)

                rows[i].style.display = 'none';

            else

                rows[i].style.display = '';

        }

    }

    this.showPage = function (pageNumber) {

        if (!inited) {

            alert("not inited");

            return;

        }

        var oldPageAnchor = document.getElementById('pg' + currentPage);

        oldPageAnchor.className = 'pg-normal';

        currentPage = pageNumber;

        var newPageAnchor = document.getElementById('pg' + currentPage);

        newPageAnchor.className = 'pg-selected';

        var from = (pageNumber - 1) * itemsPerPage + 1;

        var to = from + itemsPerPage - 1;

        this.showRecords(from, to);

    }

    this.prev = function () {

        if (currentPage > 1)

            this.showPage(currentPage - 1);

    }

    this.next = function () {

        if (currentPage < pages) {

            this.showPage(currentPage + 1);

        }

    }

    this.init = function () {

        var rows = document.getElementById(tableName).rows;

        var records = (rows.length - 1);

        pages = Math.ceil(records / itemsPerPage);

        inited = true;

    }

    this.showPageNav = function (pagerName, positionId) {

        if (!inited) {

            alert("not inited");

            return;

        }

        var element = document.getElementById(positionId);

        var pagerHtml = '<span onclick="' + pagerName + '.prev();" class="pg-normal"> « Prev </span> ';

        for (var page = 1; page <= pages; page++)

            pagerHtml += '<span id="pg' + page + '" class="pg-normal" onclick="' + pagerName + '.showPage(' + page + ');">' + page + '</span> ';

        pagerHtml += '<span onclick="' + pagerName + '.next();" class="pg-normal"> Next »</span>';

        element.innerHTML = pagerHtml;

    }

}