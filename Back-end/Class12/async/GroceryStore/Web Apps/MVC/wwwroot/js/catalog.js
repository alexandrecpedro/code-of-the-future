class Catalog {
    clickAddToBasket(el) {
        const btn = $(el);
        btn.attr('disabled', true);
        btn.removeClass('btn-success');
        btn.addClass('btn-secondary');
        let code = btn.attr('code');
        this.addToBasket(code);
    }

    addToBasket(code) {
        let token = $('[name=__RequestVerificationToken]').val();

        let headers = {};
        headers['RequestVerificationToken'] = token;

        $.ajax({
            url: '/basket/addtobasket',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(code),
            headers: headers
        }).done(function (response) {
            catalog.showSnackbar();
        });
    }

    showSnackbar() {
        var x = document.getElementById("snackbar");
        x.className = "show";
        setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);
    }
}

var catalog = new Catalog();
