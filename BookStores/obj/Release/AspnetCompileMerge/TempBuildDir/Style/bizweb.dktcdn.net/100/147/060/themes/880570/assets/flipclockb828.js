(function($){
	"user strict";
	$.fn.evo_CountDown = function( options ) {
		return this.each(function() {
			new  $.evo_CountDown( this, options );
		});
	}
	$.evo_CountDown = function( obj, options ){
		this.options = $.extend({
			autoStart			: true,
			LeadingZero:true,
			DisplayFormat:"<div><span>%%D%%</span> Days</div><div><span>%%H%%</span> Hours</div><div><span>%%M%%</span> Mins</div><div><span>%%S%%</span> Secs</div>",
			FinishMessage:"Hết hạn",
			CountActive:true,
			TargetDate:null
		}, options || {} );
		if( this.options.TargetDate == null || this.options.TargetDate == '' ){
			return ;
		}
		this.timer  = null;
		this.element = obj;
		this.CountStepper = -1;
		this.CountStepper = Math.ceil(this.CountStepper);
		this.SetTimeOutPeriod = (Math.abs(this.CountStepper)-1)*1000 + 990;
		var dthen = new Date(this.options.TargetDate);
		var dnow = new Date();
		if( this.CountStepper > 0 ) {
			ddiff = new Date(dnow-dthen);
		}
		else {
			ddiff = new Date(dthen-dnow);
		}
		gsecs = Math.floor(ddiff.valueOf()/1000);
		this.CountBack(gsecs, this);
	};
	$.evo_CountDown.fn =  $.evo_CountDown.prototype;
	$.evo_CountDown.fn.extend =  $.evo_CountDown.extend = $.extend;
	$.evo_CountDown.fn.extend({
		calculateDate:function( secs, num1, num2 ){
			var s = ((Math.floor(secs/num1))%num2).toString();
			if ( this.options.LeadingZero && s.length < 2) {
				s = "0" + s;
			}
			return "<b>" + s + "</b>";
		},
		CountBack:function( secs, self ){
			if (secs < 0) {
				self.element.innerHTML = '<div class="lof-labelexpired d-none"> '+self.options.FinishMessage+"</div>";
				return;
			}
			clearInterval(self.timer);
			DisplayStr = self.options.DisplayFormat.replace(/%%D%%/g, self.calculateDate( secs,86400,100000) );
			DisplayStr = DisplayStr.replace(/%%H%%/g, self.calculateDate(secs,3600,24));
			DisplayStr = DisplayStr.replace(/%%M%%/g, self.calculateDate(secs,60,60));
			DisplayStr = DisplayStr.replace(/%%S%%/g, self.calculateDate(secs,1,60));
			self.element.innerHTML = DisplayStr;
			if (self.options.CountActive) {
				self.timer = null;
				self.timer =  setTimeout( function(){
					self.CountBack((secs+self.CountStepper),self);
				},( self.SetTimeOutPeriod ) );
			}
		}
	});
	$(document).ready(function(){
		$('[data-countdown="countdown"]').each(function(index, el) {
			var $this = $(this);
			var $date = $this.data('date').split("-");
			$this.evo_CountDown({
				TargetDate:$date[0]+"/"+$date[1]+"/"+$date[2]+" "+$date[3]+":"+$date[4]+":"+$date[5],
				DisplayFormat:"<span class=\"block-timer\"><p>Ngày</p>%%D%%</span><span class=\"block-timer\"><p>Giờ</p>%%H%%</span><span class=\"block-timer\"><p>Phút</p>%%M%%</span><span class=\"block-timer\"><p>Giây</p>%%S%%</span>",
			FinishMessage: "Hết hạn"
			});
		});
	});
})(jQuery);