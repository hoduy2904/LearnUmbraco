const getProductDetail = (id) => {
    let result = [];
    $.ajax({
        url: '/umbraco/Surface/Product/getProducts?ids=' + id,
        method: 'GET',
        async: false,
        success: function (data) {
            result = data;
        }
    });
    return result;

}

const addCart = (id, number = 1) => {
    let carts = [];
    if (window.localStorage.getItem("carts")) {
        carts = JSON.parse(window.localStorage.getItem("carts"));
    }
    let product = carts.find(p => p.id === id);
    if (product) {
        product.number += Number(number);
        alert('Add Successfull')
    } else {
        let productDetail = getProductDetail(id);
        if (productDetail.success) {
            carts.push({
                id,
                number
            });
            alert('Add Successfull')
        } else {
            alert('Not Found Product')
        }

    }

    window.localStorage.setItem("carts", JSON.stringify(carts));
    getCountCarts();
}

const  addCartCookie = (id, number = 1) => {
    $.ajax({
        url: '/umbraco/Surface/cart/cart',
        method: 'POST',
        data: {
            id: id,
            Numbers:number
        },
        success: function (data) {
            if (data.success) { 
                alert(data.message);
                getCountCarts(data.count)
            }
        }
    })
}

const delCart = (id) => {
    if (window.localStorage.getItem("carts")) {
        let carts = JSON.parse(window.localStorage.getItem("carts"));
        console.log(carts)
        let product = carts.find(x => x.id == id);
        if (product) {
            carts=carts.filter(item=>item.id!=id);
            window.localStorage.setItem("carts", JSON.stringify(carts));
            $(`#${id}`).remove();
            getCountCarts();
        }
        else {
            alert('not found');
        }
    } else {
        alert('Not Found');
    }
}


const delCartCookie = (id) => {
    $.ajax({
        url: '/umbraco/Surface/cart/cart',
        method: 'DELETE',
        data: {
            id: id
        },
        success: function (data) {
            if (data.success) {
                alert(data.message);

                $(`#${id}`).remove();
                getCountCarts(data.count)
            }
        }
    })
}

const editCartCookie = (id, number) => {
    $.ajax({
        url: '/umbraco/Surface/cart/cart',
        method: 'PUT',
        data: {
            id: id,
            Numbers: number
        },
        success: function (data) {
            if (data.success) {
                if (number == 0) {
                    $(`#${id}`).remove();
                } else {
                    $(`#${id}`).children('td:nth-child(7)').text((data.data.Numbers * Number(data.data.price)).toLocaleString("vi-Vn"));
                }
                getCountCarts(data.count)
            }
        }
    })
}

const editCart = (id, number) => {
    if (window.localStorage.getItem("carts")) {
        let carts = JSON.parse(window.localStorage.getItem("carts"));

        let product = carts.find(x => x.id == id);
        if (product) {
            if (number == 0) {
                carts = carts.filter(product => product.id != id);
                $(`#${id}`).remove();
            }
            else {
                product.number = Number(number);
            }
            window.localStorage.setItem("carts", JSON.stringify(carts));
            let price = $(`#${id}`).children('td:nth-child(5)').text().replaceAll('.', '');
            $(`#${id}`).children('td:nth-child(7)').text((product.number * Number(price)).toLocaleString("vi-Vn"));
            getCountCarts();
        }
        else {
            alert('not Found');
        }
    } else {
        alert('not found');
    }
}

const getAllProduct = (ids) => {
    let result = [];
    $.ajax({
        url: '/umbraco/Surface/Product/getProducts?ids=' + ids.join('&ids='),
        method: 'GET',
        async: false,
        success: function (data) {
            result = data;
        }
    });
    return result;
}

const showCart = () => {
    if (window.localStorage.getItem("carts")) {
        let carts = JSON.parse(window.localStorage.getItem("carts"));
        let ids = carts.map(x => x.id);

        let allCarts = getAllProduct(ids);

        allCarts.data = allCarts.data.map(item => {
            const newDateToDate = new Date(Number(item.ToDate.match(/\d+/)[0]));
            const formatDayToDate = `${newDateToDate.getDay()}/${newDateToDate.getMonth()}/${newDateToDate.getFullYear()}`;

            const newDateFromDate = new Date(Number(item.FromDate.match(/\d+/g)[0]));
            const formatDayFromDate = `${newDateFromDate.getDay()}/${newDateFromDate.getMonth()}/${newDateFromDate.getFullYear()}`;
            return { ...item, Numbers: carts.find(x => x.id === item.Id).number, ToDate: formatDayToDate, FromDate: formatDayFromDate }
        })

        window.localStorage.setItem("carts",JSON.stringify(allCarts.data.map(item => {
            return { id: item.Id, number: item.Numbers }
        })));
        $('table tbody').html('')
        $('#CartTemplate').tmpl(allCarts).appendTo('table tbody');
        getCountCarts();
    }
}

const getCountCarts = (number=0) => {

    $('#countCart').text(number);
}
