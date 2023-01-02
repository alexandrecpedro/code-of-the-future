class Basket {
    clickIncrement(button) {
        let data = this.getData(button);
        data.Quantity++;
        this.postQuantity(data);
    }

    clickDecrement(button) {
        let data = this.getData(button);
        data.Quantity--;
        this.postQuantity(data);
    }

    updateQuantity(input) {
        let data = this.getData(input);
        this.postQuantity(data);
    }

    getData(element) {
        var itemLine = $(element).parents('[item-id]');
        var itemId = $(itemLine).attr('item-id');
        var newQuantity = $(itemLine).find('input.quantity').val();

        return {
            ProductId: itemId,
            Quantity: newQuantity
        };
    }

    postQuantity(data) {

        let token = $('[name=__RequestVerificationToken]').val();

        let headers = {};
        headers['RequestVerificationToken'] = token;

        $.ajax({
            url: '/basket/updatequantity',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data),
            headers: headers
        }).done(function (response) {
            let basketItem = response.basketItem;
            let itemLine = $('[item-id=' + basketItem.id + ']');
            itemLine.find('input').val(basketItem.quantity);
            itemLine.find('[subtotal]').html((basketItem.subtotal).twoDigits());
            let customerBasket = response.customerBasket;
            $('[item-qty]').html('Total: ' + customerBasket.items.length + ' items');
            $('[total]').html((customerBasket.total).twoDigits());

            if (basketItem.quantity === 0) {
                itemLine.remove();
            }
        });
    }
}

var basket = new Basket();

Number.prototype.twoDigits = function () {
    return this.toFixed(2).replace('.', ',');
};
