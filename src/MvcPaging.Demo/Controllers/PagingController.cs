using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using MvcPaging.Demo.Models;

namespace MvcPaging.Demo.Controllers
{
	public class PagingController : Controller
	{
		private const int DefaultPageSize = 10;
        private IList<Product> allProducts = new List<Product>();
		public PagingController()
		{
			InitializeProducts();
		}

		private void InitializeProducts()
		{
			// Create a list of products. 50% of them are in the Shoes category, 25% in the Electronics 
			// category and 25% in the Food category.
			for (var i = 0; i < 527; i++)
			{
				var product = new Product();
                product.Name = allProductNames[i % 645]; ; 
                product.Category = allCategories[i % 3];
				allProducts.Add(product);
			}
		}

		public ActionResult Index(int? page)
		{
			int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
			return View(this.allProducts.ToPagedList(currentPageIndex, DefaultPageSize));
		}

		public ActionResult CustomPageRouteValueKey(SearchModel search)
		{
			int currentPageIndex = search.page.HasValue ? search.page.Value - 1 : 0;
			return View(this.allProducts.ToPagedList(currentPageIndex, DefaultPageSize));
		}

		public ActionResult ViewByCategory(string categoryName, int? page)
		{
			categoryName = categoryName ?? this.allCategories[0];
			int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

			var productsByCategory = this.allProducts.Where(p => p.Category.Equals(categoryName)).ToPagedList(currentPageIndex,
																											  DefaultPageSize);
			ViewBag.CategoryName = new SelectList(this.allCategories, categoryName);
			ViewBag.CategoryDisplayName = categoryName;
			return View("ProductsByCategory", productsByCategory);
		}

		public ActionResult ViewByCategories(string[] categories, int? page)
		{
			var model = new ViewByCategoriesViewModel();
			model.Categories = categories ?? new string[0];
			int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

			model.Products = this.allProducts.Where(p => model.Categories.Contains(p.Category)).ToPagedList(currentPageIndex, DefaultPageSize);
			model.AvailableCategories = this.allCategories;
			return View("ProductsByCategories", model);
		}

		public ActionResult IndexAjax()
		{
			int currentPageIndex = 0;
			var products = this.allProducts.ToPagedList(currentPageIndex, DefaultPageSize);
			return View(products);
		}

		public ActionResult AjaxPage(int? page, string search)
		{
            Thread.Sleep(1500);
			ViewBag.Title = "Browse all products";
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            var products = this.allProducts.Where(x => search == null || x.Name.ToLower().Contains(search)).ToPagedList(currentPageIndex, DefaultPageSize, null, search, null, null);
            products.SearchString = search;
			return PartialView("_ProductGrid", products);
		}

		public ActionResult Bootstrap(int? page)
		{
			int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
			return View(this.allProducts.ToPagedList(currentPageIndex, DefaultPageSize));
		}

		public ActionResult Bootstrap3(int? page)
		{
			int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
			return View(this.allProducts.ToPagedList(currentPageIndex, DefaultPageSize));
		}




        private readonly string[] allCategories = new string[3] { "Shoes", "Electronics", "Food" };
        private readonly string[] allProductNames = new string[646] { "babble", "babe", "baboon", "baby ", "babysat", "babysit ", "bachelor", "back", "backbone", "backbreaking", "backdrop", "backer", "backfire", "backgammon", "background", "backhand", "backhanded", "backing", "backlash", "backlog", "backpack", "backpacker", "back seat", "backside", "backstage", "backstroke", "backtrack", "backup", "backward", "backwards", "backwoods", "backyard", "bacon", "bacteria", "bad", "bade", "badge", "badger", "badlands ", "badly", "badminton", "badmouth", "baffle", "baffled", "baffling", "bag", "bagel", "baggage", "baggy", "bag lady", "bagpipes", "bail", "bailiff", "bait", "bake", "baker", "bakery", "balance", "balanced  ", "bald", "bald eagle", "balding", "bale", "baleful", "balk", "ball  ", "balloon", "ballot", "ballpark ", "ballroom", "balls", "balm", "balmy", "baloney", "bamboo", "bamboozle", "ban", "banal", "banality", "banana", "band", "bandage", "Band-Aid", "bandanna", "bandit", "bandstand", "bandwagon", "bandy", "bane", "bang", "bangle", "bangs", "banish", "banister", "banjo", "bank", "banker", "banking", "bankrupt", "bankruptcy", "banner", "banquet", "banter", "baptism", "baptismal", "Baptist", "baptize", "bar", "barbarian", "barbaric", "barbarism", "barbecue", "barbed wire", "barbell", "barber", "barbiturate", "bar code", "bare", "bare-bones", "barefoot", "barely", "barf", "bargain", "bargaining chip", "barge", "baritone", "bark", "barley", "bar mitzvah", "barn", "barnacle", "barnyard", "barometer", "barometric", "baron", "barracks", "barrage", "barrel", "barren", "barricade", "barrier", "barring", "barrio", "barroom", "bartender", "barter", "base", "baseball", "basement", "bases", "bash", "bashful", "bashing", "basic", "basically", "basics", "basin", "basis", "bask", "basket", "basketball", "basket case", "bass", "bass guitar", "bassist", "bassoon", "bastard", "bat", "batch", "bated", "bath", "bathe", "bathing suit", "bathrobe", "bathroom", "bathtub", "bat mitzvah", "baton", "battalion", "batter", "battered", "battery", "battle", "battlefield", "battleground", "battleship", "bawdy", "bawl", "bay", "bayonet", "bayou", "bay window  ", "beach", "beach ball", "beacon", "bead", "beady", "beagle", "beak", "beaker", "beam", "bean", "bear", "bearable", "beard", "bearded ", "bear market", "beast", "beat", "beaten", "beater", "beating", "beautician", "beautiful", "beautifully", "beauty", "beauty parlor", "beaver", "bebop", "became", "because", "because of", "beckon", "become ", "bedbug", "bedclothes", "bedding", "bedlam ", "bedside", "bedspread", "bedtime", "bee", "beech", "beef", "beefy", "beehive", "beeline", "been", "beep", "beeper", "beer", "beeswax", "beet", "beetle ", "befit", "befitting", "before", "beforehand", "befriend", "beg", "began", "beggar", "begin", "beginner", "beginning ", "begun", "behalf", "behave", "behavior", "behead", "behind", "behold", "beholder", "beige", "being", "belated", "belatedly", "belch", "belie", "belief", "believable", "believe ", "belittle", "bell", "bell-bottoms", "belligerence", "bellow", "bell pepper", "belly", "bellybutton", "belong", "belongings", "beloved", "below", "belt", "beltway", "bemused", "bench", "benchmark", "bend", "beneath", "benediction ", "benefit", "benevolence", "benevolent", "benign", "bent", "bequeath", "bequest", "berate", "bereaved", "bereavement", "bereft", "beret", "berry ", "beside", "besides", "besiege", "best", "bestial", "bestiality", "best man", "bestow", "bestseller", "best-selling", "bet", "betray", "betrayal", "better", "betterment", "better off", "between", "beveled", "beverage", "beware", "bewildered", "bewildering", "bewilderment", "bewitching", "beyond", "bias", "biased", "bib", "Bible", "bible", "biblical", "bibliography", "bib overalls", "bicentennial", "biceps", "bicker", "bicycle", "bid", "bidden", "bidder", "bidding", "bide", "biennial", "bifocals", "big", "bigamist", "bigamous", "bigamy", "Big Apple", "big brother", "big cheese", "big deal", "biggie", "big league", "big-league", "bigmouth", "big name", "bigot", "bigoted", "bigotry", "big shot ", "bigwig", "bike", "biker", "bikini", "bilateral", "bilaterally", "bile", "bilingual", "bill ", "billionaire", "billionth", "Bill of Rights", "billow", "billy goat", "bimbo", "bimonthly", "bin", "binary", "bind", "binder", "binding", "binge", "bingo", "binoculars", "biochemistry", "biodegradable", "biographer", "biographical", "biography", "biological", "biologist", "biology", "biopsy", "bipartisan", "biped", "biplane", "birch", "bird", "birdbrained", "birdseed", "birth", "birth certificate", "birth control", "birth date", "birthday", "birthmark", "birthplace", "birthrate", "biscuit", "bisect ", "bite", "biting", "bitten", "bitter", "bitterly", "bitterness", "biweekly", "bizarre", "blab", "blabbermouth", "Black", "black", "black belt ", "black hole", "blackjack", "blacklist", "black magic", "blackmail", "blackmailer", "black market", "blackness ", "blandly", "blank", "blank check", "blanket", "blankly", "blankness", "blare", "blase", "blaspheme", "blasphemous", "blasphemy", "blast ", "bleak", "bleakly", "bleakness", "blearily", "bleary", "bled", "bleed", "bleeding", "blemish", "blemished", "blend", "blender ", "blind date", "blindfold", "blindly", "blindness", "blind spot", "blink", "blip", "bliss", "blissful", "blissfully", "blister", "blistering", "blithe", "blithely", "blitz", "blizzard ", "block", "blockade", "blockage", "blockbuster", "blockhead", "block letter", "blond", "blood", "bloodbath", "blood donor", "bloodhound", "bloodless", "blood pressure", "bloodshed", "bloodshot ", "blood type", "blood vessel", "bloody", "bloom", "blooper", "blossom", "blot", "blotch", "blotchy", "blotter", "blouse", "blow", "blow-dry", "blown", "blowout ", "blueberry", "bluebird", "blue blood", "blue cheese", "blue chip", "blue-collar", "bluegrass", "blue jay", "blue jeans", "blue law", "blueprint", "blues", "bluff", "bluish", "blunder", "blunt", "bluntly", "bluntness", "blur", "blurb", "blurred", "blurry", "blurt", "blush", "blusher ", "bob", "bobbin", "bobcat", "bobsled", "bodice", "bodily", "body ", "bogged down", "boggle", "bogus", "bohemian", "boil", "boiler", "boiling", "boiling point", "boisterous", "bold ", "bombed", "bomber", "bombshell", "bona fide", "bonanza", "bond", "bondage", "bone", "bone-dry ", "booby-trap", "boogie", "book", "bookcase", "bookend", "bookie", "booking", "bookkeeper", "bookkeeping", "booklet ", "boondocks", "boor", "boorish", "boost", "booster", "boot", "boot camp", "bootee", "booth", "bootleg", "bootlegger", "bootstraps", "booty", "booze", "boozer", "bop", "border", "borderline", "bore", "bored", "boredom", "boring", "born ", "botch", "both", "bother", "bothersome", "bottle", "bottled", "bottleneck", "bottom", "bottomless ", "boundary", "boundless", "bounds", "bountiful", "bounty", "bouquet", "bourbon ", "bow tie", "box", "boxcar", "boxer", "boxer shorts", "boxing", "box office", "box spring", "boy ", "braces", "bracing", "bracket", "brackish", "brag", "braggart", "braid", "Braille", "brain", "brainchild", "brainless", "brains", "brainstorm", "brainwash", "brainwashing ", "brandy", "brash", "brass", "brassiere ", "brassy", "brat", "bravado ", "brazier", "breach", "bread", "breadbasket", "breadth", "breadwinner", "break", "breakable" };

	}
}