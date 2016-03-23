//Order.js
(function (ns) {
    ns.Order = function (id, custName) {
        this.id = id;
        this.customer = new ns.Customer('A customer');
    }
})(window.bhv = window.bhv || {});
