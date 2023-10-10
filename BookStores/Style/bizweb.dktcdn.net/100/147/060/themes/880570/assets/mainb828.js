$(document).ready(function ($) {
	awe_backtotop();
	awe_category();
	$('#trigger-mobile').click(function(){
		$("#nav").toggleClass('active');
		$(this).toggleClass('active');
	});
	$('.plus-nClick1').click(function(e){
		e.preventDefault();
		$(this).parents('.has-childs').toggleClass('opened');
		$(this).parents('.has-childs').children('ul').slideToggle(100);
		$(this).parents('.has-childs').children('.mega-content').slideToggle(100);
	});
	$('.plus-nClick2').click(function(e){
		e.preventDefault();
		$(this).parents('.has-childs2').toggleClass('opened');
		$(this).parents('.has-childs2').children('ul').slideToggle(100);
	});
	$('.plus-nClick3').click(function(e){
		e.preventDefault();
		$(this).parents('.evo-mega-level1').toggleClass('opened');
		$(this).parents('.evo-mega-level1').children('.evo-mega-level2').slideToggle(100);
	});
	$('.evo-header-cart').click(function(){
		$(".cart_sidebar").toggleClass('active');
		$(".backdrop__body-backdrop___1rvky").addClass('active');
	});
	$('.backdrop__body-backdrop___1rvky, .cart_btn-close').click(function(){
		$("body").removeClass('show-search');
		$(".cart_sidebar").removeClass('active');
		$(".backdrop__body-backdrop___1rvky").removeClass('active');
	});
	$(".backdrop__body-backdrop___1rvky").removeClass('active');
	$('.ng-has-child1 a .svg1').on('click', function(e){
		e.preventDefault();var $this = $(this);
		$this.parents('.ng-has-child1').find('.ul-has-child1').stop().slideToggle();
		$(this).toggleClass('active');
		return false;
	});
	$('.ng-has-child1 .ul-has-child1 .ng-has-child2 a .svg2').on('click', function(e){
		e.preventDefault();var $this = $(this);
		$this.parents('.ng-has-child1 .ul-has-child1 .ng-has-child2').find('.ul-has-child2').stop().slideToggle();
		$(this).toggleClass('active');
		return false;
	});
	if($('.cart_body>div').length == '0' ){
		$('.cart-footer').hide();
		jQuery('<div class="cart-empty">'
			   + '<span class="empty-icon"><i class="ico ico-cart"></i></span>'
			   + '<div class="btn-cart-empty">'
			   + '<a class="btn btn-default" href="/" title="Tiếp tục mua hàng">Tiếp tục mua hàng</a>'
			   + '</div>'
			   + '</div>').appendTo('.cart_body');
	};
	$(".rte table").wrap( "<div class='table-responsive'></div>" );
	if ($('.addThis_listSharing').length > 0){
		$(window).scroll(function(){
			if(jQuery(window).scrollTop() > 100 ) {
				jQuery('.addThis_listSharing').addClass('is-show');
			} else {
				jQuery('.addThis_listSharing').removeClass('is-show');
			}
		});
	};
});
$(document).ready(function(){
	$('.evo-header .evo-header-search-form input[type="text"]').bind('focusin focusout', function(e){
		$(this).closest('.evo-searchs').toggleClass('focus', e.type == 'focusin');
	});
	var preLoadLoadGif = $('<div class="spinner-border text-primary" role="status"></div>');
	var searchTimeoutThrottle = 500;
	var searchTimeoutID = -1;
	var currReqObj = null;
	var $resultsBox = $('<div class="results-box"><div class="search-results"></div></div>').appendTo('.evo-search, .evo-searchs');
	$('.evo-header .evo-header-search-form input[type="text"]').bind('keyup change', function(){
		if($(this).val().length > 1 && $(this).val() != $(this).data('oldval')) {
			$(this).data('oldval', $(this).val());
			if(currReqObj != null) currReqObj.abort();
			clearTimeout(searchTimeoutID);
			var $form = $(this).closest('form');
			var term = 'name:(*' + $form.find('input[name="query"]').val() + '*)';
			var linkURL = $form.attr('action') + '?query=' + term + '&type=product';
			$resultsBox.find('.search-results').html('<div class="evo-loading"><div class="spinner-border text-primary" role="status"></div></div>');
			searchTimeoutID = setTimeout(function(){
				currReqObj = $.ajax({
					url: $form.attr('action'),
					async: false,
					data: {
						type: 'product',
						view: 'json',
						q: term
					},
					dataType: 'json',
					success: function(data){
						currReqObj = null;
						if(data.results_total == 0) {
							$resultsBox.find('.search-results').html('<div class="note">Không có kết quả tìm kiếm</div>');
						} else {
							$resultsBox.find('.search-results').empty();
							$.each(data.results, function(index, item){
								var xshow = 'wholesale';
								if(!(item.title.toLowerCase().indexOf(xshow) > -1)) {
									var $row = $('<a class="clearfix"></a>').attr('href', item.url).attr('title', item.title);
									$row.append('<div class="img"><img src="' + item.thumb + '" /></div>');
									$row.append('<div class="d-title">'+item.title+'</div>');
									$row.append('<div class="d-title d-price">'+item.price+'</div>');
									$resultsBox.find('.search-results').append($row);
								}
							});
							$resultsBox.find('.search-results').append("<a href='" + linkURL + "' class='note' title='Xem tất cả'>Xem tất cả</a>");
						}
					}
				});
			}, searchTimeoutThrottle);
		} else if ($(this).val().length <= 1) {
			$resultsBox.find('.search-results').empty();
		}
	}).attr('autocomplete', 'off').data('oldval', '').bind('focusin', function(){
		$resultsBox.fadeIn(200);
	}).bind('click', function(e){
		e.stopPropagation();
	});
	$('body').bind('click', function(){
		$resultsBox.fadeOut(200);
	});
	$('.evo-search-form').on('submit', function(e){
		e.preventDefault();
		var term = 'name:(*' + $(this).find('input[name="query"]').val() + '*)';
		var linkURL = $(this).attr('action') + '?query=' + term + '&type=product';
		window.location = linkURL;
	});
});
$(document).on('click','.overlay, .close-popup, .btn-continue, .fancybox-close', function() {   
	hidePopup('.awe-popup'); 	
	setTimeout(function(){$('.loading').removeClass('loaded-content');},500);
	return false;
})
function awe_showNoitice(selector) {
	$(selector).animate({right: '0'}, 500);
	setTimeout(function(){$(selector).animate({right: '-300px'}, 500);}, 3500);
}  window.awe_showNoitice=awe_showNoitice;
function awe_showLoading(selector) {
	var loading = $('.loader').html();
	$(selector).addClass("loading").append(loading); 
}  window.awe_showLoading=awe_showLoading;
function awe_hideLoading(selector) {
	$(selector).removeClass("loading"); 
	$(selector + ' .loading-icon').remove();
}  window.awe_hideLoading=awe_hideLoading;
function awe_showPopup(selector) {
	$(selector).addClass('active');
}  window.awe_showPopup=awe_showPopup;
function awe_hidePopup(selector) {
	$(selector).removeClass('active');
}  window.awe_hidePopup=awe_hidePopup;
function awe_convertVietnamese(str) { 
	str= str.toLowerCase();str= str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g,"a");str= str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g,"e");str= str.replace(/ì|í|ị|ỉ|ĩ/g,"i");str= str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g,"o"); str= str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g,"u");str= str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g,"y");str= str.replace(/đ/g,"d"); str= str.replace(/!|@|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\.|\:|\;|\'| |\"|\&|\#|\[|\]|~|$|_/g,"-");str= str.replace(/-+-/g,"-");str= str.replace(/^\-+|\-+$/g,""); 
	return str; 
} window.awe_convertVietnamese=awe_convertVietnamese;
function awe_category(){
	$('.nav-category .Collapsible__Plus').click(function(e){
		$(this).parent().toggleClass('active');
	});
} window.awe_category=awe_category;
function awe_backtotop() { 
	if ($('.back-to-top').length) {
		var scrollTrigger = 100,
			backToTop = function () {
				var scrollTop = $(window).scrollTop();
				if (scrollTop > scrollTrigger) {
					$('.back-to-top').addClass('show');
				} else {
					$('.back-to-top').removeClass('show');
				}
			};
		backToTop();
		$(window).on('scroll', function () {
			backToTop();
		});
		$('.back-to-top').on('click', function (e) {
			e.preventDefault();
			$('html,body').animate({
				scrollTop: 0
			}, 700);
		});
	}
} window.awe_backtotop=awe_backtotop;
$('.btn-close').click(function() {
	$(this).parents('.dropdown').toggleClass('open');
}); 
$(document).on('keydown','#qty, #quantity-detail, .number-sidebar, .number-phone',function(e){-1!==$.inArray(e.keyCode,[46,8,9,27,13,110,190])||/65|67|86|88/.test(e.keyCode)&&(!0===e.ctrlKey||!0===e.metaKey)||35<=e.keyCode&&40>=e.keyCode||(e.shiftKey||48>e.keyCode||57<e.keyCode)&&(96>e.keyCode||105<e.keyCode)&&e.preventDefault()});
var buy_now = function(id) {
	var quantity = 1;
	var params = {
		type: 'POST',
		url: '/cart/add.js',
		data: 'quantity=' + quantity + '&variantId=' + id,
		dataType: 'json',
		success: function(line_item) {
			window.location = '/checkout';
		},
		error: function(XMLHttpRequest, textStatus) {
			Bizweb.onError(XMLHttpRequest, textStatus);
		}
	};
	jQuery.ajax(params);
}
window.theme = window.theme || {};
theme.wishlist = (function (){
	var wishlistButtonClass = '.js-btn-wishlist',
		wishlistRemoveButtonClass = '.js-remove-wishlist',
		$wishlistCount = $('.js-wishlist-count'),
		$wishlistContainer = $('.js-wishlist-content'),
		$wishlistSmall = $('.wish-list-small'),
		wishlistViewAll = $('.wish-list-button-all'),
		wishlistObject = JSON.parse(localStorage.getItem('localWishlist')) || [],
		wishlistPageUrl = $('.js-wishlist-link').attr('href'),
		loadNoResult = function (){
			$wishlistContainer.html('<div class="col text-center"><div class="alert alert-warning d-inline-block"><h3>Sản phẩm nào của chúng tôi bạn mong muốn sở hữu nhất?</h3><p>Hãy thêm vào danh sách sản phẩm yêu thích ngay nhé!</p></div></div>');
			$wishlistSmall.html('<div class="empty-description"><span class="empty-icon"><i class="ico ico-favorite-heart"></i></span><div class="empty-text"><h3>Sản phẩm nào của chúng tôi bạn mong muốn sở hữu nhất?</h3><p>Hãy thêm vào danh sách sản phẩm yêu thích ngay nhé!</p></div></div><style>.container--wishlist .js-wishlist-content{border:none;}</style>');
			wishlistViewAll.addClass('d-none');
		};
	function loadWishlist(){
		$wishlistContainer.html('');
		if (wishlistObject.length > 0){
			var recentview_wishlist = [];
			for (var i = 0; i < wishlistObject.length; i++) { 
				var productHandle = wishlistObject[i];
				for (var i = 0; i < wishlistObject.length; i++) { 
					var productHandle = wishlistObject[i];
					var wishlist = new Promise(function(resolve, reject) {
						$.ajax({
							url:'/' + productHandle + '?view=wishlist',
							success: function(product){
								resolve(product);
							},
							error: function(err){
								resolve('');
							}
						})
					});
					recentview_wishlist.push(wishlist);
				}
				Promise.all(recentview_wishlist).then(function(values) {
					$.each(values, function(i, v){
						$('.js-wishlist-content').append(v);
					});
					awe_lazyloadImage();
					$(".evo-product-block-item .thumbs-list .thumbs-list-item img").click(function () {
						var t = $(this).attr("data-img");
						$(this).parent().siblings().removeClass("active"), $(this).parent().addClass("active");
						var e = $(this).parents(".evo-product-block-item ").find(".evo-image-pro img");
						e && $(e).attr("src", t);
					});
				});
			}
		}else{
			loadNoResult();
		}
		$wishlistCount.text(wishlistObject.length);
		$(wishlistButtonClass).each(function(){
			var productHandle = $(this).data('handle');
			var iconWishlist = $.inArray(productHandle,wishlistObject) !== -1 ? "<svg x='0px' y='0px' viewBox='0 0 512 512' style='enable-background:new 0 0 512 512;' xml:space='preserve'><path d='M376,30c-27.783,0-53.255,8.804-75.707,26.168c-21.525,16.647-35.856,37.85-44.293,53.268 c-8.437-15.419-22.768-36.621-44.293-53.268C189.255,38.804,163.783,30,136,30C58.468,30,0,93.417,0,177.514 c0,90.854,72.943,153.015,183.369,247.118c18.752,15.981,40.007,34.095,62.099,53.414C248.38,480.596,252.12,482,256,482 s7.62-1.404,10.532-3.953c22.094-19.322,43.348-37.435,62.111-53.425C439.057,330.529,512,268.368,512,177.514 C512,93.417,453.532,30,376,30z'/></svg>" : "<svg x='0px' y='0px' viewBox='0 0 512 512' style='enable-background:new 0 0 512 512;' xml:space='preserve'><path d='M474.644,74.27C449.391,45.616,414.358,29.836,376,29.836c-53.948,0-88.103,32.22-107.255,59.25 c-4.969,7.014-9.196,14.047-12.745,20.665c-3.549-6.618-7.775-13.651-12.745-20.665c-19.152-27.03-53.307-59.25-107.255-59.25 c-38.358,0-73.391,15.781-98.645,44.435C13.267,101.605,0,138.213,0,177.351c0,42.603,16.633,82.228,52.345,124.7 c31.917,37.96,77.834,77.088,131.005,122.397c19.813,16.884,40.302,34.344,62.115,53.429l0.655,0.574 c2.828,2.476,6.354,3.713,9.88,3.713s7.052-1.238,9.88-3.713l0.655-0.574c21.813-19.085,42.302-36.544,62.118-53.431 c53.168-45.306,99.085-84.434,131.002-122.395C495.367,259.578,512,219.954,512,177.351 C512,138.213,498.733,101.605,474.644,74.27z M309.193,401.614c-17.08,14.554-34.658,29.533-53.193,45.646 c-18.534-16.111-36.113-31.091-53.196-45.648C98.745,312.939,30,254.358,30,177.351c0-31.83,10.605-61.394,29.862-83.245 C79.34,72.007,106.379,59.836,136,59.836c41.129,0,67.716,25.338,82.776,46.594c13.509,19.064,20.558,38.282,22.962,45.659 c2.011,6.175,7.768,10.354,14.262,10.354c6.494,0,12.251-4.179,14.262-10.354c2.404-7.377,9.453-26.595,22.962-45.66 c15.06-21.255,41.647-46.593,82.776-46.593c29.621,0,56.66,12.171,76.137,34.27C471.395,115.957,482,145.521,482,177.351 C482,254.358,413.255,312.939,309.193,401.614z'/></svg>";
			var textWishlist = $.inArray(productHandle,wishlistObject) !== -1 ? "Đến trang sản phẩm yêu thích" : "Thêm vào yêu thích";
			$(this).html(iconWishlist).attr('title',textWishlist);
		});
	}
	function updateWishlist(self) {
		var productHandle = $(self).data('handle'),
			allSimilarProduct = $(wishlistButtonClass+'[data-handle="'+productHandle+'"]');
		var isAdded = $.inArray(productHandle,wishlistObject) !== -1 ? true:false;
		if (isAdded) {
			// go to wishlist page
			window.location.href = wishlistPageUrl;
		}else{
			wishlistObject.push(productHandle);
			allSimilarProduct.fadeOut('slow').fadeIn('fast').html(theme.strings.wishlistIconAdded)
			$('.tooltip-inner').text(theme.strings.wishlistTextAdded);
		}
		localStorage.setItem('localWishlist', JSON.stringify(wishlistObject)); 
		$wishlistCount.text(wishlistObject.length);
	};
	$(document).on('click',wishlistButtonClass,function (event) {
		event.preventDefault();
		updateWishlist(this);
	});
	$(document).on('click',wishlistRemoveButtonClass,function(){
		var productHandle = $(this).data('handle'),
			allSimilarProduct = $(wishlistButtonClass+'[data-handle="'+productHandle+'"]');

		//update button
		allSimilarProduct.html(theme.strings.wishlistIcon)
		//update tooltip
		allSimilarProduct.attr('data-original-title',theme.strings.wishlistText);
		$('.tooltip-inner').text(theme.strings.wishlistText);
		//update Object
		wishlistObject.splice(wishlistObject.indexOf(productHandle), 1);
		localStorage.setItem('localWishlist', JSON.stringify(wishlistObject)); 
		// Remove element
		$(this).closest('.js-wishlist-item').fadeOut(); // or .remove()
		//count
		$wishlistCount.text(wishlistObject.length);
		if (wishlistObject.length === 0) {
			loadNoResult();
		}
	});

	loadWishlist();
	$(document).on('shopify:section:load', loadWishlist);
	return{
		load:loadWishlist
	}
})()
theme.alert = (function(){
	var $alert = $('#js-global-alert'),
		$title = $('#js-global-alert .alert-heading'),
		$content = $('#js-global-alert .alert-content'),
		close = '#js-global-alert .close';
	$(document).on('click',close,function(){
		$alert.removeClass('active');
	});
	function createAlert(title,mess,time,type){
		var alertTitle = title || '',
			showTime = time || 3000,
			alertClass = type || 'alert-success';
		$alert.removeClass('alert-success').removeClass('alert-danger').removeClass('alert-warning')
		$alert.addClass(alertClass);
		$title.html(title);
		$content.html(mess);
		$alert.addClass('active');
		setTimeout(function(){
			$alert.removeClass('active');
		}, showTime); 
	}
	return{
		new:createAlert
	}
})()
theme.strings = {
	wishlistNoResult: "<h3>Sản phẩm nào của chúng tôi bạn mong muốn sở hữu nhất?</h3><p>Hãy thêm vào danh sách sản phẩm yêu thích ngay nhé!</p>",
	wishlistIcon: "Yêu thích",
	wishlistIconAdded: "<svg x='0px' y='0px' viewBox='0 0 512 512' style='enable-background:new 0 0 512 512;' xml:space='preserve'><path d='M376,30c-27.783,0-53.255,8.804-75.707,26.168c-21.525,16.647-35.856,37.85-44.293,53.268 c-8.437-15.419-22.768-36.621-44.293-53.268C189.255,38.804,163.783,30,136,30C58.468,30,0,93.417,0,177.514 c0,90.854,72.943,153.015,183.369,247.118c18.752,15.981,40.007,34.095,62.099,53.414C248.38,480.596,252.12,482,256,482 s7.62-1.404,10.532-3.953c22.094-19.322,43.348-37.435,62.111-53.425C439.057,330.529,512,268.368,512,177.514 C512,93.417,453.532,30,376,30z'/></svg>",
	wishlistText: "Thêm vào yêu thích",
	wishlistTextAdded: "Đến trang sản phẩm yêu thích",
	wishlistRemove: "Xóa",
	compareNoResult: "Vui lòng chọn sản phẩm để so sánh",
	compareIcon: "<svg version='1.1' xmlns='http://www.w3.org/2000/svg' xmlns:xlink='http://www.w3.org/1999/xlink' x='0px' y='0px' viewBox='0 0 477.867 477.867' style='enable-background:new 0 0 477.867 477.867;' xml:space='preserve'><path d='M409.6,0c-9.426,0-17.067,7.641-17.067,17.067v62.344C304.667-5.656,164.478-3.386,79.411,84.479c-40.09,41.409-62.455,96.818-62.344,154.454c0,9.426,7.641,17.067,17.067,17.067S51.2,248.359,51.2,238.933c0.021-103.682,84.088-187.717,187.771-187.696c52.657,0.01,102.888,22.135,138.442,60.976l-75.605,25.207c-8.954,2.979-13.799,12.652-10.82,21.606s12.652,13.799,21.606,10.82l102.4-34.133c6.99-2.328,11.697-8.88,11.674-16.247v-102.4C426.667,7.641,419.026,0,409.6,0z'/><path d='M443.733,221.867c-9.426,0-17.067,7.641-17.067,17.067c-0.021,103.682-84.088,187.717-187.771,187.696c-52.657-0.01-102.888-22.135-138.442-60.976l75.605-25.207c8.954-2.979,13.799-12.652,10.82-21.606c-2.979-8.954-12.652-13.799-21.606-10.82l-102.4,34.133c-6.99,2.328-11.697,8.88-11.674,16.247v102.4c0,9.426,7.641,17.067,17.067,17.067s17.067-7.641,17.067-17.067v-62.345c87.866,85.067,228.056,82.798,313.122-5.068c40.09-41.409,62.455-96.818,62.344-154.454C460.8,229.508,453.159,221.867,443.733,221.867z'/></svg>",
	compareText: "So sánh",
	compareRemove: "Xóa khỏi danh sách",
	compareNotifyAdded: 'Đã thêm vào danh sách so sánh',
	compareNotifyRemoved: "Đã xóa khỏi dánh sách so sánh",
	compareNotifyMaximum: "So sánh tối đa 4 sản phẩm",
};