using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdlife.web.Services.Implements
{
    public class PinYinConverter : IPinYinConverter
    {
        public char GetCharCapitalPinYin(char ch)
        {
            return GetCharPinYin(ch)[0];
        }

        public string GetStringCapitalPinYin(string str, string spliter = "")
        {
            if (str == null) return null;
            return string.Join(spliter, str.Select(x => GetCharCapitalPinYin(x)));
        }

        public string GetCharPinYin(char ch)
        {
            if (ch <= '\x00FF' ||
                ch < '\x4E00' || ch > '\x9FA5' ||
                char.IsWhiteSpace(ch) ||
                char.IsPunctuation(ch) || char.IsSeparator(ch))
                return ch.ToString();

#if DNXCORE50
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
#endif
            var gbBytes = Encoding.GetEncoding("GB2312").GetBytes(ch.ToString());
            var gbCode = gbBytes[0] * 256 + gbBytes[1] - 65536;

            if (gbCode > 0 && gbCode < 160)
                return ch.ToString();
            if (gbCode > LastCode || gbCode < FirstCode)
                return ch.ToString();

            if (gbCode <= LevelOneLastCode)
            {
                for (int pos = 11; pos >= 0; pos--)
                {
                    int endPos = pos * 33;
                    if (gbCode >= PinYinCodes[endPos])
                    {
                        for (int i = endPos + 32; i >= endPos; i--)
                        {
                            if (PinYinCodes[i] <= gbCode)
                            {
                                return PinYinNames[i];
                            }
                        }
                        break;
                    }
                }
            }
            else
            {
                int pos = Level2Chinese.IndexOf(ch);
                if (pos != -1)
                {
                    return Level2PinYin[pos];
                }
            }

            return ch.ToString();
        }

        public string GetStringPinYin(string str, string spliter = "")
        {
            if (str == null) return null;
            return string.Join(spliter, str.Select(ch => GetCharPinYin(ch)));
        }

        private static readonly short[] PinYinCodes = new short[]
        {
            -20319,-20317,-20304,-20295,-20292,-20283,-20265,-20257,-20242,-20230,-20051,-20036,-20032,-20026,-20002,-19990,-19986,-19982,
            -19976,-19805,-19784,-19775,-19774,-19763,-19756,-19751,-19746,-19741,-19739,-19728,-19725,-19715,-19540,-19531,-19525,-19515,
            -19500,-19484,-19479,-19467,-19289,-19288,-19281,-19275,-19270,-19263,-19261,-19249,-19243,-19242,-19238,-19235,-19227,-19224,
            -19218,-19212,-19038,-19023,-19018,-19006,-19003,-18996,-18977,-18961,-18952,-18783,-18774,-18773,-18763,-18756,-18741,-18735,
            -18731,-18722,-18710,-18697,-18696,-18526,-18518,-18501,-18490,-18478,-18463,-18448,-18447,-18446,-18239,-18237,-18231,-18220,
            -18211,-18201,-18184,-18183,-18181,-18012,-17997,-17988,-17970,-17964,-17961,-17950,-17947,-17931,-17928,-17922,-17759,-17752,
            -17733,-17730,-17721,-17703,-17701,-17697,-17692,-17683,-17676,-17496,-17487,-17482,-17468,-17454,-17433,-17427,-17417,-17202,
            -17185,-16983,-16970,-16942,-16915,-16733,-16708,-16706,-16689,-16664,-16657,-16647,-16474,-16470,-16465,-16459,-16452,-16448,
            -16433,-16429,-16427,-16423,-16419,-16412,-16407,-16403,-16401,-16393,-16220,-16216,-16212,-16205,-16202,-16187,-16180,-16171,
            -16169,-16158,-16155,-15959,-15958,-15944,-15933,-15920,-15915,-15903,-15889,-15878,-15707,-15701,-15681,-15667,-15661,-15659,
            -15652,-15640,-15631,-15625,-15454,-15448,-15436,-15435,-15419,-15416,-15408,-15394,-15385,-15377,-15375,-15369,-15363,-15362,
            -15183,-15180,-15165,-15158,-15153,-15150,-15149,-15144,-15143,-15141,-15140,-15139,-15128,-15121,-15119,-15117,-15110,-15109,
            -14941,-14937,-14933,-14930,-14929,-14928,-14926,-14922,-14921,-14914,-14908,-14902,-14894,-14889,-14882,-14873,-14871,-14857,
            -14678,-14674,-14670,-14668,-14663,-14654,-14645,-14630,-14594,-14429,-14407,-14399,-14384,-14379,-14368,-14355,-14353,-14345,
            -14170,-14159,-14151,-14149,-14145,-14140,-14137,-14135,-14125,-14123,-14122,-14112,-14109,-14099,-14097,-14094,-14092,-14090,
            -14087,-14083,-13917,-13914,-13910,-13907,-13906,-13905,-13896,-13894,-13878,-13870,-13859,-13847,-13831,-13658,-13611,-13601,
            -13406,-13404,-13400,-13398,-13395,-13391,-13387,-13383,-13367,-13359,-13356,-13343,-13340,-13329,-13326,-13318,-13147,-13138,
            -13120,-13107,-13096,-13095,-13091,-13076,-13068,-13063,-13060,-12888,-12875,-12871,-12860,-12858,-12852,-12849,-12838,-12831,
            -12829,-12812,-12802,-12607,-12597,-12594,-12585,-12556,-12359,-12346,-12320,-12300,-12120,-12099,-12089,-12074,-12067,-12058,
            -12039,-11867,-11861,-11847,-11831,-11798,-11781,-11604,-11589,-11536,-11358,-11340,-11339,-11324,-11303,-11097,-11077,-11067,
            -11055,-11052,-11045,-11041,-11038,-11024,-11020,-11019,-11018,-11014,-10838,-10832,-10815,-10800,-10790,-10780,-10764,-10587,
            -10544,-10533,-10519,-10331,-10329,-10328,-10322,-10315,-10309,-10307,-10296,-10281,-10274,-10270,-10262,-10260,-10256,-10254
        };

        private static readonly string[] PinYinNames = new string[]
        {
            "A","Ai","An","Ang","Ao","Ba","Bai","Ban","Bang","Bao","Bei","Ben","Beng","Bi","Bian","Biao","Bie","Bin","Bing",
            "Bo","Bu","Ba","Cai","Can","Cang","Cao","Ce","Ceng","Cha","Chai","Chan","Chang","Chao","Che","Chen","Cheng","Chi",
            "Chong","Chou","Chu","Chuai","Chuan","Chuang","Chui","Chun","Chuo","Ci","Cong","Cou","Cu","Cuan","Cui","Cun","Cuo",
            "Da","Dai","Dan","Dang","Dao","De","Deng","Di","Dian","Diao","Die","Ding","Diu","Dong","Dou","Du","Duan","Dui",
            "Dun","Duo","E","En","Er","Fa","Fan","Fang","Fei","Fen","Feng","Fo","Fou","Fu","Ga","Gai","Gan","Gang","Gao","Ge",
            "Gei","Gen","Geng","Gong","Gou","Gu","Gua","Guai","Guan","Guang","Gui","Gun","Guo","Ha","Hai","Han","Hang","Hao",
            "He","Hei","Hen","Heng","Hong","Hou","Hu","Hua","Huai","Huan","Huang","Hui","Hun","Huo","Ji","Jia","Jian","Jiang",
            "Jiao","Jie","Jin","Jing","Jiong","Jiu","Ju","Juan","Jue","Jun","Ka","Kai","Kan","Kang","Kao","Ke","Ken","Keng",
            "Kong","Kou","Ku","Kua","Kuai","Kuan","Kuang","Kui","Kun","Kuo","La","Lai","Lan","Lang","Lao","Le","Lei","Leng",
            "Li","Lia","Lian","Liang","Liao","Lie","Lin","Ling","Liu","Long","Lou","Lu","Lv","Luan","Lue","Lun","Luo","Ma",
            "Mai","Man","Mang","Mao","Me","Mei","Men","Meng","Mi","Mian","Miao","Mie","Min","Ming","Miu","Mo","Mou","Mu","Na",
            "Nai","Nan","Nang","Nao","Ne","Nei","Nen","Neng","Ni","Nian","Niang","Niao","Nie","Nin","Ning","Niu","Nong","Nu",
            "Nv","Nuan","Nue","Nuo","O","Ou","Pa","Pai","Pan","Pang","Pao","Pei","Pen","Peng","Pi","Pian","Piao","Pie","Pin",
            "Ping","Po","Pu","Qi","Qia","Qian","Qiang","Qiao","Qie","Qin","Qing","Qiong","Qiu","Qu","Quan","Que","Qun","Ran",
            "Rang","Rao","Re","Ren","Reng","Ri","Rong","Rou","Ru","Ruan","Rui","Run","Ruo","Sa","Sai","San","Sang","Sao","Se",
            "Sen","Seng","Sha","Shai","Shan","Shang","Shao","She","Shen","Sheng","Shi","Shou","Shu","Shua","Shuai","Shuan",
            "Shuang","Shui","Shun","Shuo","Si","Song","Sou","Su","Suan","Sui","Sun","Suo","Ta","Tai","Tan","Tang","Tao","Te",
            "Teng","Ti","Tian","Tiao","Tie","Ting","Tong","Tou","Tu","Tuan","Tui","Tun","Tuo","Wa","Wai","Wan","Wang","Wei",
            "Wen","Weng","Wo","Wu","Xi","Xia","Xian","Xiang","Xiao","Xie","Xin","Xing","Xiong","Xiu","Xu","Xuan","Xue","Xun",
            "Ya","Yan","Yang","Yao","Ye","Yi","Yin","Ying","Yo","Yong","You","Yu","Yuan","Yue","Yun","Za","Zai","Zan","Zang",
            "Zao","Ze","Zei","Zen","Zeng","Zha","Zhai","Zhan","Zhang","Zhao","Zhe","Zhen","Zheng","Zhi","Zhong","Zhou","Zhu",
            "Zhua","Zhuai","Zhuan","Zhuang","Zhui","Zhun","Zhuo","Zi","Zong","Zou","Zu","Zuan","Zui","Zun","Zuo"
        };

        private static readonly string Level2Chinese =
            "亍丌兀丐廿卅丕亘丞鬲孬噩丨禺丿匕乇夭爻卮氐囟胤馗毓睾鼗丶亟鼐乜乩亓芈孛啬嘏仄厍厝厣厥厮靥赝匚叵匦" +
            "匮匾赜卦卣刂刈刎刭刳刿剀剌剞剡剜蒯剽劂劁劐劓冂罔亻仃仉仂仨仡仫仞伛仳伢佤仵伥伧伉伫佞佧攸佚佝佟佗" +
            "伲伽佶佴侑侉侃侏佾佻侪佼侬侔俦俨俪俅俚俣俜俑俟俸倩偌俳倬倏倮倭俾倜倌倥倨偾偃偕偈偎偬偻傥傧傩傺僖" +
            "儆僭僬僦僮儇儋仝氽佘佥俎龠汆籴兮巽黉馘冁夔勹匍訇匐凫夙兕亠兖亳衮袤亵脔裒禀嬴蠃羸冫冱冽冼凇冖冢冥" +
            "讠讦讧讪讴讵讷诂诃诋诏诎诒诓诔诖诘诙诜诟诠诤诨诩诮诰诳诶诹诼诿谀谂谄谇谌谏谑谒谔谕谖谙谛谘谝谟谠" +
            "谡谥谧谪谫谮谯谲谳谵谶卩卺阝阢阡阱阪阽阼陂陉陔陟陧陬陲陴隈隍隗隰邗邛邝邙邬邡邴邳邶邺邸邰郏郅邾郐" +
            "郄郇郓郦郢郜郗郛郫郯郾鄄鄢鄞鄣鄱鄯鄹酃酆刍奂劢劬劭劾哿勐勖勰叟燮矍廴凵凼鬯厶弁畚巯坌垩垡塾墼壅壑" +
            "圩圬圪圳圹圮圯坜圻坂坩垅坫垆坼坻坨坭坶坳垭垤垌垲埏垧垴垓垠埕埘埚埙埒垸埴埯埸埤埝堋堍埽埭堀堞堙塄" +
            "堠塥塬墁墉墚墀馨鼙懿艹艽艿芏芊芨芄芎芑芗芙芫芸芾芰苈苊苣芘芷芮苋苌苁芩芴芡芪芟苄苎芤苡茉苷苤茏茇" +
            "苜苴苒苘茌苻苓茑茚茆茔茕苠苕茜荑荛荜茈莒茼茴茱莛荞茯荏荇荃荟荀茗荠茭茺茳荦荥荨茛荩荬荪荭荮莰荸莳" +
            "莴莠莪莓莜莅荼莶莩荽莸荻莘莞莨莺莼菁萁菥菘堇萘萋菝菽菖萜萸萑萆菔菟萏萃菸菹菪菅菀萦菰菡葜葑葚葙葳" +
            "蒇蒈葺蒉葸萼葆葩葶蒌蒎萱葭蓁蓍蓐蓦蒽蓓蓊蒿蒺蓠蒡蒹蒴蒗蓥蓣蔌甍蔸蓰蔹蔟蔺蕖蔻蓿蓼蕙蕈蕨蕤蕞蕺瞢蕃" +
            "蕲蕻薤薨薇薏蕹薮薜薅薹薷薰藓藁藜藿蘧蘅蘩蘖蘼廾弈夼奁耷奕奚奘匏尢尥尬尴扌扪抟抻拊拚拗拮挢拶挹捋捃" +
            "掭揶捱捺掎掴捭掬掊捩掮掼揲揸揠揿揄揞揎摒揆掾摅摁搋搛搠搌搦搡摞撄摭撖摺撷撸撙撺擀擐擗擤擢攉攥攮弋" +
            "忒甙弑卟叱叽叩叨叻吒吖吆呋呒呓呔呖呃吡呗呙吣吲咂咔呷呱呤咚咛咄呶呦咝哐咭哂咴哒咧咦哓哔呲咣哕咻咿" +
            "哌哙哚哜咩咪咤哝哏哞唛哧唠哽唔哳唢唣唏唑唧唪啧喏喵啉啭啁啕唿啐唼唷啖啵啶啷唳唰啜喋嗒喃喱喹喈喁喟" +
            "啾嗖喑啻嗟喽喾喔喙嗪嗷嗉嘟嗑嗫嗬嗔嗦嗝嗄嗯嗥嗲嗳嗌嗍嗨嗵嗤辔嘞嘈嘌嘁嘤嘣嗾嘀嘧嘭噘嘹噗嘬噍噢噙噜" +
            "噌噔嚆噤噱噫噻噼嚅嚓嚯囔囗囝囡囵囫囹囿圄圊圉圜帏帙帔帑帱帻帼帷幄幔幛幞幡岌屺岍岐岖岈岘岙岑岚岜岵" +
            "岢岽岬岫岱岣峁岷峄峒峤峋峥崂崃崧崦崮崤崞崆崛嵘崾崴崽嵬嵛嵯嵝嵫嵋嵊嵩嵴嶂嶙嶝豳嶷巅彳彷徂徇徉後徕" +
            "徙徜徨徭徵徼衢彡犭犰犴犷犸狃狁狎狍狒狨狯狩狲狴狷猁狳猃狺狻猗猓猡猊猞猝猕猢猹猥猬猸猱獐獍獗獠獬獯" +
            "獾舛夥飧夤夂饣饧饨饩饪饫饬饴饷饽馀馄馇馊馍馐馑馓馔馕庀庑庋庖庥庠庹庵庾庳赓廒廑廛廨廪膺忄忉忖忏怃" +
            "忮怄忡忤忾怅怆忪忭忸怙怵怦怛怏怍怩怫怊怿怡恸恹恻恺恂恪恽悖悚悭悝悃悒悌悛惬悻悱惝惘惆惚悴愠愦愕愣" +
            "惴愀愎愫慊慵憬憔憧憷懔懵忝隳闩闫闱闳闵闶闼闾阃阄阆阈阊阋阌阍阏阒阕阖阗阙阚丬爿戕氵汔汜汊沣沅沐沔" +
            "沌汨汩汴汶沆沩泐泔沭泷泸泱泗沲泠泖泺泫泮沱泓泯泾洹洧洌浃浈洇洄洙洎洫浍洮洵洚浏浒浔洳涑浯涞涠浞涓" +
            "涔浜浠浼浣渚淇淅淞渎涿淠渑淦淝淙渖涫渌涮渫湮湎湫溲湟溆湓湔渲渥湄滟溱溘滠漭滢溥溧溽溻溷滗溴滏溏滂" +
            "溟潢潆潇漤漕滹漯漶潋潴漪漉漩澉澍澌潸潲潼潺濑濉澧澹澶濂濡濮濞濠濯瀚瀣瀛瀹瀵灏灞宀宄宕宓宥宸甯骞搴" +
            "寤寮褰寰蹇謇辶迓迕迥迮迤迩迦迳迨逅逄逋逦逑逍逖逡逵逶逭逯遄遑遒遐遨遘遢遛暹遴遽邂邈邃邋彐彗彖彘尻" +
            "咫屐屙孱屣屦羼弪弩弭艴弼鬻屮妁妃妍妩妪妣妗姊妫妞妤姒妲妯姗妾娅娆姝娈姣姘姹娌娉娲娴娑娣娓婀婧婊婕" +
            "娼婢婵胬媪媛婷婺媾嫫媲嫒嫔媸嫠嫣嫱嫖嫦嫘嫜嬉嬗嬖嬲嬷孀尕尜孚孥孳孑孓孢驵驷驸驺驿驽骀骁骅骈骊骐骒" +
            "骓骖骘骛骜骝骟骠骢骣骥骧纟纡纣纥纨纩纭纰纾绀绁绂绉绋绌绐绔绗绛绠绡绨绫绮绯绱绲缍绶绺绻绾缁缂缃缇" +
            "缈缋缌缏缑缒缗缙缜缛缟缡缢缣缤缥缦缧缪缫缬缭缯缰缱缲缳缵幺畿巛甾邕玎玑玮玢玟珏珂珑玷玳珀珉珈珥珙" +
            "顼琊珩珧珞玺珲琏琪瑛琦琥琨琰琮琬琛琚瑁瑜瑗瑕瑙瑷瑭瑾璜璎璀璁璇璋璞璨璩璐璧瓒璺韪韫韬杌杓杞杈杩枥" +
            "枇杪杳枘枧杵枨枞枭枋杷杼柰栉柘栊柩枰栌柙枵柚枳柝栀柃枸柢栎柁柽栲栳桠桡桎桢桄桤梃栝桕桦桁桧桀栾桊" +
            "桉栩梵梏桴桷梓桫棂楮棼椟椠棹椤棰椋椁楗棣椐楱椹楠楂楝榄楫榀榘楸椴槌榇榈槎榉楦楣楹榛榧榻榫榭槔榱槁" +
            "槊槟榕槠榍槿樯槭樗樘橥槲橄樾檠橐橛樵檎橹樽樨橘橼檑檐檩檗檫猷獒殁殂殇殄殒殓殍殚殛殡殪轫轭轱轲轳轵" +
            "轶轸轷轹轺轼轾辁辂辄辇辋辍辎辏辘辚軎戋戗戛戟戢戡戥戤戬臧瓯瓴瓿甏甑甓攴旮旯旰昊昙杲昃昕昀炅曷昝昴" +
            "昱昶昵耆晟晔晁晏晖晡晗晷暄暌暧暝暾曛曜曦曩贲贳贶贻贽赀赅赆赈赉赇赍赕赙觇觊觋觌觎觏觐觑牮犟牝牦牯" +
            "牾牿犄犋犍犏犒挈挲掰搿擘耄毪毳毽毵毹氅氇氆氍氕氘氙氚氡氩氤氪氲攵敕敫牍牒牖爰虢刖肟肜肓肼朊肽肱肫" +
            "肭肴肷胧胨胩胪胛胂胄胙胍胗朐胝胫胱胴胭脍脎胲胼朕脒豚脶脞脬脘脲腈腌腓腴腙腚腱腠腩腼腽腭腧塍媵膈膂" +
            "膑滕膣膪臌朦臊膻臁膦欤欷欹歃歆歙飑飒飓飕飙飚殳彀毂觳斐齑斓於旆旄旃旌旎旒旖炀炜炖炝炻烀炷炫炱烨烊" +
            "焐焓焖焯焱煳煜煨煅煲煊煸煺熘熳熵熨熠燠燔燧燹爝爨灬焘煦熹戾戽扃扈扉礻祀祆祉祛祜祓祚祢祗祠祯祧祺禅" +
            "禊禚禧禳忑忐怼恝恚恧恁恙恣悫愆愍慝憩憝懋懑戆肀聿沓泶淼矶矸砀砉砗砘砑斫砭砜砝砹砺砻砟砼砥砬砣砩硎" +
            "硭硖硗砦硐硇硌硪碛碓碚碇碜碡碣碲碹碥磔磙磉磬磲礅磴礓礤礞礴龛黹黻黼盱眄眍盹眇眈眚眢眙眭眦眵眸睐睑" +
            "睇睃睚睨睢睥睿瞍睽瞀瞌瞑瞟瞠瞰瞵瞽町畀畎畋畈畛畲畹疃罘罡罟詈罨罴罱罹羁罾盍盥蠲钅钆钇钋钊钌钍钏钐" +
            "钔钗钕钚钛钜钣钤钫钪钭钬钯钰钲钴钶钷钸钹钺钼钽钿铄铈铉铊铋铌铍铎铐铑铒铕铖铗铙铘铛铞铟铠铢铤铥铧" +
            "铨铪铩铫铮铯铳铴铵铷铹铼铽铿锃锂锆锇锉锊锍锎锏锒锓锔锕锖锘锛锝锞锟锢锪锫锩锬锱锲锴锶锷锸锼锾锿镂" +
            "锵镄镅镆镉镌镎镏镒镓镔镖镗镘镙镛镞镟镝镡镢镤镥镦镧镨镩镪镫镬镯镱镲镳锺矧矬雉秕秭秣秫稆嵇稃稂稞稔" +
            "稹稷穑黏馥穰皈皎皓皙皤瓞瓠甬鸠鸢鸨鸩鸪鸫鸬鸲鸱鸶鸸鸷鸹鸺鸾鹁鹂鹄鹆鹇鹈鹉鹋鹌鹎鹑鹕鹗鹚鹛鹜鹞鹣鹦" +
            "鹧鹨鹩鹪鹫鹬鹱鹭鹳疒疔疖疠疝疬疣疳疴疸痄疱疰痃痂痖痍痣痨痦痤痫痧瘃痱痼痿瘐瘀瘅瘌瘗瘊瘥瘘瘕瘙瘛瘼" +
            "瘢瘠癀瘭瘰瘿瘵癃瘾瘳癍癞癔癜癖癫癯翊竦穸穹窀窆窈窕窦窠窬窨窭窳衤衩衲衽衿袂袢裆袷袼裉裢裎裣裥裱褚" +
            "裼裨裾裰褡褙褓褛褊褴褫褶襁襦襻疋胥皲皴矜耒耔耖耜耠耢耥耦耧耩耨耱耋耵聃聆聍聒聩聱覃顸颀颃颉颌颍颏" +
            "颔颚颛颞颟颡颢颥颦虍虔虬虮虿虺虼虻蚨蚍蚋蚬蚝蚧蚣蚪蚓蚩蚶蛄蚵蛎蚰蚺蚱蚯蛉蛏蚴蛩蛱蛲蛭蛳蛐蜓蛞蛴蛟" +
            "蛘蛑蜃蜇蛸蜈蜊蜍蜉蜣蜻蜞蜥蜮蜚蜾蝈蜴蜱蜩蜷蜿螂蜢蝽蝾蝻蝠蝰蝌蝮螋蝓蝣蝼蝤蝙蝥螓螯螨蟒蟆螈螅螭螗螃" +
            "螫蟥螬螵螳蟋蟓螽蟑蟀蟊蟛蟪蟠蟮蠖蠓蟾蠊蠛蠡蠹蠼缶罂罄罅舐竺竽笈笃笄笕笊笫笏筇笸笪笙笮笱笠笥笤笳笾" +
            "笞筘筚筅筵筌筝筠筮筻筢筲筱箐箦箧箸箬箝箨箅箪箜箢箫箴篑篁篌篝篚篥篦篪簌篾篼簏簖簋簟簪簦簸籁籀臾舁" +
            "舂舄臬衄舡舢舣舭舯舨舫舸舻舳舴舾艄艉艋艏艚艟艨衾袅袈裘裟襞羝羟羧羯羰羲籼敉粑粝粜粞粢粲粼粽糁糇糌" +
            "糍糈糅糗糨艮暨羿翎翕翥翡翦翩翮翳糸絷綦綮繇纛麸麴赳趄趔趑趱赧赭豇豉酊酐酎酏酤酢酡酰酩酯酽酾酲酴酹" +
            "醌醅醐醍醑醢醣醪醭醮醯醵醴醺豕鹾趸跫踅蹙蹩趵趿趼趺跄跖跗跚跞跎跏跛跆跬跷跸跣跹跻跤踉跽踔踝踟踬踮" +
            "踣踯踺蹀踹踵踽踱蹉蹁蹂蹑蹒蹊蹰蹶蹼蹯蹴躅躏躔躐躜躞豸貂貊貅貘貔斛觖觞觚觜觥觫觯訾謦靓雩雳雯霆霁霈" +
            "霏霎霪霭霰霾龀龃龅龆龇龈龉龊龌黾鼋鼍隹隼隽雎雒瞿雠銎銮鋈錾鍪鏊鎏鐾鑫鱿鲂鲅鲆鲇鲈稣鲋鲎鲐鲑鲒鲔鲕" +
            "鲚鲛鲞鲟鲠鲡鲢鲣鲥鲦鲧鲨鲩鲫鲭鲮鲰鲱鲲鲳鲴鲵鲶鲷鲺鲻鲼鲽鳄鳅鳆鳇鳊鳋鳌鳍鳎鳏鳐鳓鳔鳕鳗鳘鳙鳜鳝鳟" +
            "鳢靼鞅鞑鞒鞔鞯鞫鞣鞲鞴骱骰骷鹘骶骺骼髁髀髅髂髋髌髑魅魃魇魉魈魍魑飨餍餮饕饔髟髡髦髯髫髻髭髹鬈鬏鬓" +
            "鬟鬣麽麾縻麂麇麈麋麒鏖麝麟黛黜黝黠黟黢黩黧黥黪黯鼢鼬鼯鼹鼷鼽鼾齄";

        private static readonly string[] Level2PinYin = new string[]
        {
            "Chu","Ji","Wu","Gai","Nian","Sa","Pi","Gen","Cheng","Ge","Nao","E","Shu","Yu","Pie","Bi",
            "Tuo","Yao","Yao","Zhi","Di","Xin","Yin","Kui","Yu","Gao","Tao","Dian","Ji","Nai","Nie","Ji",
            "Qi","Mi","Bei","Se","Gu","Ze","She","Cuo","Yan","Jue","Si","Ye","Yan","Fang","Po","Gui",
            "Kui","Bian","Ze","Gua","You","Ce","Yi","Wen","Jing","Ku","Gui","Kai","La","Ji","Yan","Wan",
            "Kuai","Piao","Jue","Qiao","Huo","Yi","Tong","Wang","Dan","Ding","Zhang","Le","Sa","Yi","Mu","Ren",
            "Yu","Pi","Ya","Wa","Wu","Chang","Cang","Kang","Zhu","Ning","Ka","You","Yi","Gou","Tong","Tuo",
            "Ni","Ga","Ji","Er","You","Kua","Kan","Zhu","Yi","Tiao","Chai","Jiao","Nong","Mou","Chou","Yan",
            "Li","Qiu","Li","Yu","Ping","Yong","Si","Feng","Qian","Ruo","Pai","Zhuo","Shu","Luo","Wo","Bi",
            "Ti","Guan","Kong","Ju","Fen","Yan","Xie","Ji","Wei","Zong","Lou","Tang","Bin","Nuo","Chi","Xi",
            "Jing","Jian","Jiao","Jiu","Tong","Xuan","Dan","Tong","Tun","She","Qian","Zu","Yue","Cuan","Di","Xi",
            "Xun","Hong","Guo","Chan","Kui","Bao","Pu","Hong","Fu","Fu","Su","Si","Wen","Yan","Bo","Gun",
            "Mao","Xie","Luan","Pou","Bing","Ying","Luo","Lei","Liang","Hu","Lie","Xian","Song","Ping","Zhong","Ming",
            "Yan","Jie","Hong","Shan","Ou","Ju","Ne","Gu","He","Di","Zhao","Qu","Dai","Kuang","Lei","Gua",
            "Jie","Hui","Shen","Gou","Quan","Zheng","Hun","Xu","Qiao","Gao","Kuang","Ei","Zou","Zhuo","Wei","Yu",
            "Shen","Chan","Sui","Chen","Jian","Xue","Ye","E","Yu","Xuan","An","Di","Zi","Pian","Mo","Dang",
            "Su","Shi","Mi","Zhe","Jian","Zen","Qiao","Jue","Yan","Zhan","Chen","Dan","Jin","Zuo","Wu","Qian",
            "Jing","Ban","Yan","Zuo","Bei","Jing","Gai","Zhi","Nie","Zou","Chui","Pi","Wei","Huang","Wei","Xi",
            "Han","Qiong","Kuang","Mang","Wu","Fang","Bing","Pi","Bei","Ye","Di","Tai","Jia","Zhi","Zhu","Kuai",
            "Qie","Xun","Yun","Li","Ying","Gao","Xi","Fu","Pi","Tan","Yan","Juan","Yan","Yin","Zhang","Po",
            "Shan","Zou","Ling","Feng","Chu","Huan","Mai","Qu","Shao","He","Ge","Meng","Xu","Xie","Sou","Xie",
            "Jue","Jian","Qian","Dang","Chang","Si","Bian","Ben","Qiu","Ben","E","Fa","Shu","Ji","Yong","He",
            "Wei","Wu","Ge","Zhen","Kuang","Pi","Yi","Li","Qi","Ban","Gan","Long","Dian","Lu","Che","Di",
            "Tuo","Ni","Mu","Ao","Ya","Die","Dong","Kai","Shan","Shang","Nao","Gai","Yin","Cheng","Shi","Guo",
            "Xun","Lie","Yuan","Zhi","An","Yi","Pi","Nian","Peng","Tu","Sao","Dai","Ku","Die","Yin","Leng",
            "Hou","Ge","Yuan","Man","Yong","Liang","Chi","Xin","Pi","Yi","Cao","Jiao","Nai","Du","Qian","Ji",
            "Wan","Xiong","Qi","Xiang","Fu","Yuan","Yun","Fei","Ji","Li","E","Ju","Pi","Zhi","Rui","Xian",
            "Chang","Cong","Qin","Wu","Qian","Qi","Shan","Bian","Zhu","Kou","Yi","Mo","Gan","Pie","Long","Ba",
            "Mu","Ju","Ran","Qing","Chi","Fu","Ling","Niao","Yin","Mao","Ying","Qiong","Min","Tiao","Qian","Yi",
            "Rao","Bi","Zi","Ju","Tong","Hui","Zhu","Ting","Qiao","Fu","Ren","Xing","Quan","Hui","Xun","Ming",
            "Qi","Jiao","Chong","Jiang","Luo","Ying","Qian","Gen","Jin","Mai","Sun","Hong","Zhou","Kan","Bi","Shi",
            "Wo","You","E","Mei","You","Li","Tu","Xian","Fu","Sui","You","Di","Shen","Guan","Lang","Ying",
            "Chun","Jing","Qi","Xi","Song","Jin","Nai","Qi","Ba","Shu","Chang","Tie","Yu","Huan","Bi","Fu",
            "Tu","Dan","Cui","Yan","Zu","Dang","Jian","Wan","Ying","Gu","Han","Qia","Feng","Shen","Xiang","Wei",
            "Chan","Kai","Qi","Kui","Xi","E","Bao","Pa","Ting","Lou","Pai","Xuan","Jia","Zhen","Shi","Ru",
            "Mo","En","Bei","Weng","Hao","Ji","Li","Bang","Jian","Shuo","Lang","Ying","Yu","Su","Meng","Dou",
            "Xi","Lian","Cu","Lin","Qu","Kou","Xu","Liao","Hui","Xun","Jue","Rui","Zui","Ji","Meng","Fan",
            "Qi","Hong","Xie","Hong","Wei","Yi","Weng","Sou","Bi","Hao","Tai","Ru","Xun","Xian","Gao","Li",
            "Huo","Qu","Heng","Fan","Nie","Mi","Gong","Yi","Kuang","Lian","Da","Yi","Xi","Zang","Pao","You",
            "Liao","Ga","Gan","Ti","Men","Tuan","Chen","Fu","Pin","Niu","Jie","Jiao","Za","Yi","Lv","Jun",
            "Tian","Ye","Ai","Na","Ji","Guo","Bai","Ju","Pou","Lie","Qian","Guan","Die","Zha","Ya","Qin",
            "Yu","An","Xuan","Bing","Kui","Yuan","Shu","En","Chuai","Jian","Shuo","Zhan","Nuo","Sang","Luo","Ying",
            "Zhi","Han","Zhe","Xie","Lu","Zun","Cuan","Gan","Huan","Pi","Xing","Zhuo","Huo","Zuan","Nang","Yi",
            "Te","Dai","Shi","Bu","Chi","Ji","Kou","Dao","Le","Zha","A","Yao","Fu","Mu","Yi","Tai",
            "Li","E","Bi","Bei","Guo","Qin","Yin","Za","Ka","Ga","Gua","Ling","Dong","Ning","Duo","Nao",
            "You","Si","Kuang","Ji","Shen","Hui","Da","Lie","Yi","Xiao","Bi","Ci","Guang","Yue","Xiu","Yi",
            "Pai","Kuai","Duo","Ji","Mie","Mi","Zha","Nong","Gen","Mou","Mai","Chi","Lao","Geng","En","Zha",
            "Suo","Zao","Xi","Zuo","Ji","Feng","Ze","Nuo","Miao","Lin","Zhuan","Zhou","Tao","Hu","Cui","Sha",
            "Yo","Dan","Bo","Ding","Lang","Li","Shua","Chuo","Die","Da","Nan","Li","Kui","Jie","Yong","Kui",
            "Jiu","Sou","Yin","Chi","Jie","Lou","Ku","Wo","Hui","Qin","Ao","Su","Du","Ke","Nie","He",
            "Chen","Suo","Ge","A","En","Hao","Dia","Ai","Ai","Suo","Hei","Tong","Chi","Pei","Lei","Cao",
            "Piao","Qi","Ying","Beng","Sou","Di","Mi","Peng","Jue","Liao","Pu","Chuai","Jiao","O","Qin","Lu",
            "Ceng","Deng","Hao","Jin","Jue","Yi","Sai","Pi","Ru","Cha","Huo","Nang","Wei","Jian","Nan","Lun",
            "Hu","Ling","You","Yu","Qing","Yu","Huan","Wei","Zhi","Pei","Tang","Dao","Ze","Guo","Wei","Wo",
            "Man","Zhang","Fu","Fan","Ji","Qi","Qian","Qi","Qu","Ya","Xian","Ao","Cen","Lan","Ba","Hu",
            "Ke","Dong","Jia","Xiu","Dai","Gou","Mao","Min","Yi","Dong","Qiao","Xun","Zheng","Lao","Lai","Song",
            "Yan","Gu","Xiao","Guo","Kong","Jue","Rong","Yao","Wai","Zai","Wei","Yu","Cuo","Lou","Zi","Mei",
            "Sheng","Song","Ji","Zhang","Lin","Deng","Bin","Yi","Dian","Chi","Pang","Cu","Xun","Yang","Hou","Lai",
            "Xi","Chang","Huang","Yao","Zheng","Jiao","Qu","San","Fan","Qiu","An","Guang","Ma","Niu","Yun","Xia",
            "Pao","Fei","Rong","Kuai","Shou","Sun","Bi","Juan","Li","Yu","Xian","Yin","Suan","Yi","Guo","Luo",
            "Ni","She","Cu","Mi","Hu","Cha","Wei","Wei","Mei","Nao","Zhang","Jing","Jue","Liao","Xie","Xun",
            "Huan","Chuan","Huo","Sun","Yin","Dong","Shi","Tang","Tun","Xi","Ren","Yu","Chi","Yi","Xiang","Bo",
            "Yu","Hun","Zha","Sou","Mo","Xiu","Jin","San","Zhuan","Nang","Pi","Wu","Gui","Pao","Xiu","Xiang",
            "Tuo","An","Yu","Bi","Geng","Ao","Jin","Chan","Xie","Lin","Ying","Shu","Dao","Cun","Chan","Wu",
            "Zhi","Ou","Chong","Wu","Kai","Chang","Chuang","Song","Bian","Niu","Hu","Chu","Peng","Da","Yang","Zuo",
            "Ni","Fu","Chao","Yi","Yi","Tong","Yan","Ce","Kai","Xun","Ke","Yun","Bei","Song","Qian","Kui",
            "Kun","Yi","Ti","Quan","Qie","Xing","Fei","Chang","Wang","Chou","Hu","Cui","Yun","Kui","E","Leng",
            "Zhui","Qiao","Bi","Su","Qie","Yong","Jing","Qiao","Chong","Chu","Lin","Meng","Tian","Hui","Shuan","Yan",
            "Wei","Hong","Min","Kang","Ta","Lv","Kun","Jiu","Lang","Yu","Chang","Xi","Wen","Hun","E","Qu",
            "Que","He","Tian","Que","Kan","Jiang","Pan","Qiang","San","Qi","Si","Cha","Feng","Yuan","Mu","Mian",
            "Dun","Mi","Gu","Bian","Wen","Hang","Wei","Le","Gan","Shu","Long","Lu","Yang","Si","Duo","Ling",
            "Mao","Luo","Xuan","Pan","Duo","Hong","Min","Jing","Huan","Wei","Lie","Jia","Zhen","Yin","Hui","Zhu",
            "Ji","Xu","Hui","Tao","Xun","Jiang","Liu","Hu","Xun","Ru","Su","Wu","Lai","Wei","Zhuo","Juan",
            "Cen","Bang","Xi","Mei","Huan","Zhu","Qi","Xi","Song","Du","Zhuo","Pei","Mian","Gan","Fei","Cong",
            "Shen","Guan","Lu","Shuan","Xie","Yan","Mian","Qiu","Sou","Huang","Xu","Pen","Jian","Xuan","Wo","Mei",
            "Yan","Qin","Ke","She","Mang","Ying","Pu","Li","Ru","Ta","Hun","Bi","Xiu","Fu","Tang","Pang",
            "Ming","Huang","Ying","Xiao","Lan","Cao","Hu","Luo","Huan","Lian","Zhu","Yi","Lu","Xuan","Gan","Shu",
            "Si","Shan","Shao","Tong","Chan","Lai","Sui","Li","Dan","Chan","Lian","Ru","Pu","Bi","Hao","Zhuo",
            "Han","Xie","Ying","Yue","Fen","Hao","Ba","Bao","Gui","Dang","Mi","You","Chen","Ning","Jian","Qian",
            "Wu","Liao","Qian","Huan","Jian","Jian","Zou","Ya","Wu","Jiong","Ze","Yi","Er","Jia","Jing","Dai",
            "Hou","Pang","Bu","Li","Qiu","Xiao","Ti","Qun","Kui","Wei","Huan","Lu","Chuan","Huang","Qiu","Xia",
            "Ao","Gou","Ta","Liu","Xian","Lin","Ju","Xie","Miao","Sui","La","Ji","Hui","Tuan","Zhi","Kao",
            "Zhi","Ji","E","Chan","Xi","Ju","Chan","Jing","Nu","Mi","Fu","Bi","Yu","Che","Shuo","Fei",
            "Yan","Wu","Yu","Bi","Jin","Zi","Gui","Niu","Yu","Si","Da","Zhou","Shan","Qie","Ya","Rao",
            "Shu","Luan","Jiao","Pin","Cha","Li","Ping","Wa","Xian","Suo","Di","Wei","E","Jing","Biao","Jie",
            "Chang","Bi","Chan","Nu","Ao","Yuan","Ting","Wu","Gou","Mo","Pi","Ai","Pin","Chi","Li","Yan",
            "Qiang","Piao","Chang","Lei","Zhang","Xi","Shan","Bi","Niao","Mo","Shuang","Ga","Ga","Fu","Nu","Zi",
            "Jie","Jue","Bao","Zang","Si","Fu","Zou","Yi","Nu","Dai","Xiao","Hua","Pian","Li","Qi","Ke",
            "Zhui","Can","Zhi","Wu","Ao","Liu","Shan","Biao","Cong","Chan","Ji","Xiang","Jiao","Yu","Zhou","Ge",
            "Wan","Kuang","Yun","Pi","Shu","Gan","Xie","Fu","Zhou","Fu","Chu","Dai","Ku","Hang","Jiang","Geng",
            "Xiao","Ti","Ling","Qi","Fei","Shang","Gun","Duo","Shou","Liu","Quan","Wan","Zi","Ke","Xiang","Ti",
            "Miao","Hui","Si","Bian","Gou","Zhui","Min","Jin","Zhen","Ru","Gao","Li","Yi","Jian","Bin","Piao",
            "Man","Lei","Miao","Sao","Xie","Liao","Zeng","Jiang","Qian","Qiao","Huan","Zuan","Yao","Ji","Chuan","Zai",
            "Yong","Ding","Ji","Wei","Bin","Min","Jue","Ke","Long","Dian","Dai","Po","Min","Jia","Er","Gong",
            "Xu","Ya","Heng","Yao","Luo","Xi","Hui","Lian","Qi","Ying","Qi","Hu","Kun","Yan","Cong","Wan",
            "Chen","Ju","Mao","Yu","Yuan","Xia","Nao","Ai","Tang","Jin","Huang","Ying","Cui","Cong","Xuan","Zhang",
            "Pu","Can","Qu","Lu","Bi","Zan","Wen","Wei","Yun","Tao","Wu","Shao","Qi","Cha","Ma","Li",
            "Pi","Miao","Yao","Rui","Jian","Chu","Cheng","Cong","Xiao","Fang","Pa","Zhu","Nai","Zhi","Zhe","Long",
            "Jiu","Ping","Lu","Xia","Xiao","You","Zhi","Tuo","Zhi","Ling","Gou","Di","Li","Tuo","Cheng","Kao",
            "Lao","Ya","Rao","Zhi","Zhen","Guang","Qi","Ting","Gua","Jiu","Hua","Heng","Gui","Jie","Luan","Juan",
            "An","Xu","Fan","Gu","Fu","Jue","Zi","Suo","Ling","Chu","Fen","Du","Qian","Zhao","Luo","Chui",
            "Liang","Guo","Jian","Di","Ju","Cou","Zhen","Nan","Zha","Lian","Lan","Ji","Pin","Ju","Qiu","Duan",
            "Chui","Chen","Lv","Cha","Ju","Xuan","Mei","Ying","Zhen","Fei","Ta","Sun","Xie","Gao","Cui","Gao",
            "Shuo","Bin","Rong","Zhu","Xie","Jin","Qiang","Qi","Chu","Tang","Zhu","Hu","Gan","Yue","Qing","Tuo",
            "Jue","Qiao","Qin","Lu","Zun","Xi","Ju","Yuan","Lei","Yan","Lin","Bo","Cha","You","Ao","Mo",
            "Cu","Shang","Tian","Yun","Lian","Piao","Dan","Ji","Bin","Yi","Ren","E","Gu","Ke","Lu","Zhi",
            "Yi","Zhen","Hu","Li","Yao","Shi","Zhi","Quan","Lu","Zhe","Nian","Wang","Chuo","Zi","Cou","Lu",
            "Lin","Wei","Jian","Qiang","Jia","Ji","Ji","Kan","Deng","Gai","Jian","Zang","Ou","Ling","Bu","Beng",
            "Zeng","Pi","Po","Ga","La","Gan","Hao","Tan","Gao","Ze","Xin","Yun","Gui","He","Zan","Mao",
            "Yu","Chang","Ni","Qi","Sheng","Ye","Chao","Yan","Hui","Bu","Han","Gui","Xuan","Kui","Ai","Ming",
            "Tun","Xun","Yao","Xi","Nang","Ben","Shi","Kuang","Yi","Zhi","Zi","Gai","Jin","Zhen","Lai","Qiu",
            "Ji","Dan","Fu","Chan","Ji","Xi","Di","Yu","Gou","Jin","Qu","Jian","Jiang","Pin","Mao","Gu",
            "Wu","Gu","Ji","Ju","Jian","Pian","Kao","Qie","Suo","Bai","Ge","Bo","Mao","Mu","Cui","Jian",
            "San","Shu","Chang","Lu","Pu","Qu","Pie","Dao","Xian","Chuan","Dong","Ya","Yin","Ke","Yun","Fan",
            "Chi","Jiao","Du","Die","You","Yuan","Guo","Yue","Wo","Rong","Huang","Jing","Ruan","Tai","Gong","Zhun",
            "Na","Yao","Qian","Long","Dong","Ka","Lu","Jia","Shen","Zhou","Zuo","Gua","Zhen","Qu","Zhi","Jing",
            "Guang","Dong","Yan","Kuai","Sa","Hai","Pian","Zhen","Mi","Tun","Luo","Cuo","Pao","Wan","Niao","Jing",
            "Yan","Fei","Yu","Zong","Ding","Jian","Cou","Nan","Mian","Wa","E","Shu","Cheng","Ying","Ge","Lv",
            "Bin","Teng","Zhi","Chuai","Gu","Meng","Sao","Shan","Lian","Lin","Yu","Xi","Qi","Sha","Xin","Xi",
            "Biao","Sa","Ju","Sou","Biao","Biao","Shu","Gou","Gu","Hu","Fei","Ji","Lan","Yu","Pei","Mao",
            "Zhan","Jing","Ni","Liu","Yi","Yang","Wei","Dun","Qiang","Shi","Hu","Zhu","Xuan","Tai","Ye","Yang",
            "Wu","Han","Men","Chao","Yan","Hu","Yu","Wei","Duan","Bao","Xuan","Bian","Tui","Liu","Man","Shang",
            "Yun","Yi","Yu","Fan","Sui","Xian","Jue","Cuan","Huo","Tao","Xu","Xi","Li","Hu","Jiong","Hu",
            "Fei","Shi","Si","Xian","Zhi","Qu","Hu","Fu","Zuo","Mi","Zhi","Ci","Zhen","Tiao","Qi","Chan",
            "Xi","Zhuo","Xi","Rang","Te","Tan","Dui","Jia","Hui","Nv","Nin","Yang","Zi","Que","Qian","Min",
            "Te","Qi","Dui","Mao","Men","Gang","Yu","Yu","Ta","Xue","Miao","Ji","Gan","Dang","Hua","Che",
            "Dun","Ya","Zhuo","Bian","Feng","Fa","Ai","Li","Long","Zha","Tong","Di","La","Tuo","Fu","Xing",
            "Mang","Xia","Qiao","Zhai","Dong","Nao","Ge","Wo","Qi","Dui","Bei","Ding","Chen","Zhou","Jie","Di",
            "Xuan","Bian","Zhe","Gun","Sang","Qing","Qu","Dun","Deng","Jiang","Ca","Meng","Bo","Kan","Zhi","Fu",
            "Fu","Xu","Mian","Kou","Dun","Miao","Dan","Sheng","Yuan","Yi","Sui","Zi","Chi","Mou","Lai","Jian",
            "Di","Suo","Ya","Ni","Sui","Pi","Rui","Sou","Kui","Mao","Ke","Ming","Piao","Cheng","Kan","Lin",
            "Gu","Ding","Bi","Quan","Tian","Fan","Zhen","She","Wan","Tuan","Fu","Gang","Gu","Li","Yan","Pi",
            "Lan","Li","Ji","Zeng","He","Guan","Juan","Jin","Ga","Yi","Po","Zhao","Liao","Tu","Chuan","Shan",
            "Men","Chai","Nv","Bu","Tai","Ju","Ban","Qian","Fang","Kang","Dou","Huo","Ba","Yu","Zheng","Gu",
            "Ke","Po","Bu","Bo","Yue","Mu","Tan","Dian","Shuo","Shi","Xuan","Ta","Bi","Ni","Pi","Duo",
            "Kao","Lao","Er","You","Cheng","Jia","Nao","Ye","Cheng","Diao","Yin","Kai","Zhu","Ding","Diu","Hua",
            "Quan","Ha","Sha","Diao","Zheng","Se","Chong","Tang","An","Ru","Lao","Lai","Te","Keng","Zeng","Li",
            "Gao","E","Cuo","Lve","Liu","Kai","Jian","Lang","Qin","Ju","A","Qiang","Nuo","Ben","De","Ke",
            "Kun","Gu","Huo","Pei","Juan","Tan","Zi","Qie","Kai","Si","E","Cha","Sou","Huan","Ai","Lou",
            "Qiang","Fei","Mei","Mo","Ge","Juan","Na","Liu","Yi","Jia","Bin","Biao","Tang","Man","Luo","Yong",
            "Chuo","Xuan","Di","Tan","Jue","Pu","Lu","Dui","Lan","Pu","Cuan","Qiang","Deng","Huo","Zhuo","Yi",
            "Cha","Biao","Zhong","Shen","Cuo","Zhi","Bi","Zi","Mo","Shu","Lv","Ji","Fu","Lang","Ke","Ren",
            "Zhen","Ji","Se","Nian","Fu","Rang","Gui","Jiao","Hao","Xi","Po","Die","Hu","Yong","Jiu","Yuan",
            "Bao","Zhen","Gu","Dong","Lu","Qu","Chi","Si","Er","Zhi","Gua","Xiu","Luan","Bo","Li","Hu",
            "Yu","Xian","Ti","Wu","Miao","An","Bei","Chun","Hu","E","Ci","Mei","Wu","Yao","Jian","Ying",
            "Zhe","Liu","Liao","Jiao","Jiu","Yu","Hu","Lu","Guan","Bing","Ding","Jie","Li","Shan","Li","You",
            "Gan","Ke","Da","Zha","Pao","Zhu","Xuan","Jia","Ya","Yi","Zhi","Lao","Wu","Cuo","Xian","Sha",
            "Zhu","Fei","Gu","Wei","Yu","Yu","Dan","La","Yi","Hou","Chai","Lou","Jia","Sao","Chi","Mo",
            "Ban","Ji","Huang","Biao","Luo","Ying","Zhai","Long","Yin","Chou","Ban","Lai","Yi","Dian","Pi","Dian",
            "Qu","Yi","Song","Xi","Qiong","Zhun","Bian","Yao","Tiao","Dou","Ke","Yu","Xun","Ju","Yu","Yi",
            "Cha","Na","Ren","Jin","Mei","Pan","Dang","Jia","Ge","Ken","Lian","Cheng","Lian","Jian","Biao","Chu",
            "Ti","Bi","Ju","Duo","Da","Bei","Bao","Lv","Bian","Lan","Chi","Zhe","Qiang","Ru","Pan","Ya",
            "Xu","Jun","Cun","Jin","Lei","Zi","Chao","Si","Huo","Lao","Tang","Ou","Lou","Jiang","Nou","Mo",
            "Die","Ding","Dan","Ling","Ning","Guo","Kui","Ao","Qin","Han","Qi","Hang","Jie","He","Ying","Ke",
            "Han","E","Zhuan","Nie","Man","Sang","Hao","Ru","Pin","Hu","Qian","Qiu","Ji","Chai","Hui","Ge",
            "Meng","Fu","Pi","Rui","Xian","Hao","Jie","Gong","Dou","Yin","Chi","Han","Gu","Ke","Li","You",
            "Ran","Zha","Qiu","Ling","Cheng","You","Qiong","Jia","Nao","Zhi","Si","Qu","Ting","Kuo","Qi","Jiao",
            "Yang","Mou","Shen","Zhe","Shao","Wu","Li","Chu","Fu","Qiang","Qing","Qi","Xi","Yu","Fei","Guo",
            "Guo","Yi","Pi","Tiao","Quan","Wan","Lang","Meng","Chun","Rong","Nan","Fu","Kui","Ke","Fu","Sou",
            "Yu","You","Lou","You","Bian","Mou","Qin","Ao","Man","Mang","Ma","Yuan","Xi","Chi","Tang","Pang",
            "Shi","Huang","Cao","Piao","Tang","Xi","Xiang","Zhong","Zhang","Shuai","Mao","Peng","Hui","Pan","Shan","Huo",
            "Meng","Chan","Lian","Mie","Li","Du","Qu","Fou","Ying","Qing","Xia","Shi","Zhu","Yu","Ji","Du",
            "Ji","Jian","Zhao","Zi","Hu","Qiong","Po","Da","Sheng","Ze","Gou","Li","Si","Tiao","Jia","Bian",
            "Chi","Kou","Bi","Xian","Yan","Quan","Zheng","Jun","Shi","Gang","Pa","Shao","Xiao","Qing","Ze","Qie",
            "Zhu","Ruo","Qian","Tuo","Bi","Dan","Kong","Wan","Xiao","Zhen","Kui","Huang","Hou","Gou","Fei","Li",
            "Bi","Chi","Su","Mie","Dou","Lu","Duan","Gui","Dian","Zan","Deng","Bo","Lai","Zhou","Yu","Yu",
            "Chong","Xi","Nie","Nv","Chuan","Shan","Yi","Bi","Zhong","Ban","Fang","Ge","Lu","Zhu","Ze","Xi",
            "Shao","Wei","Meng","Shou","Cao","Chong","Meng","Qin","Niao","Jia","Qiu","Sha","Bi","Di","Qiang","Suo",
            "Jie","Tang","Xi","Xian","Mi","Ba","Li","Tiao","Xi","Zi","Can","Lin","Zong","San","Hou","Zan",
            "Ci","Xu","Rou","Qiu","Jiang","Gen","Ji","Yi","Ling","Xi","Zhu","Fei","Jian","Pian","He","Yi",
            "Jiao","Zhi","Qi","Qi","Yao","Dao","Fu","Qu","Jiu","Ju","Lie","Zi","Zan","Nan","Zhe","Jiang",
            "Chi","Ding","Gan","Zhou","Yi","Gu","Zuo","Tuo","Xian","Ming","Zhi","Yan","Shai","Cheng","Tu","Lei",
            "Kun","Pei","Hu","Ti","Xu","Hai","Tang","Lao","Bu","Jiao","Xi","Ju","Li","Xun","Shi","Cuo",
            "Dun","Qiong","Xue","Cu","Bie","Bo","Ta","Jian","Fu","Qiang","Zhi","Fu","Shan","Li","Tuo","Jia",
            "Bo","Tai","Kui","Qiao","Bi","Xian","Xian","Ji","Jiao","Liang","Ji","Chuo","Huai","Chi","Zhi","Dian",
            "Bo","Zhi","Jian","Die","Chuai","Zhong","Ju","Duo","Cuo","Pian","Rou","Nie","Pan","Qi","Chu","Jue",
            "Pu","Fan","Cu","Zhu","Lin","Chan","Lie","Zuan","Xie","Zhi","Diao","Mo","Xiu","Mo","Pi","Hu",
            "Jue","Shang","Gu","Zi","Gong","Su","Zhi","Zi","Qing","Liang","Yu","Li","Wen","Ting","Ji","Pei",
            "Fei","Sha","Yin","Ai","Xian","Mai","Chen","Ju","Bao","Tiao","Zi","Yin","Yu","Chuo","Wo","Mian",
            "Yuan","Tuo","Zhui","Sun","Jun","Ju","Luo","Qu","Chou","Qiong","Luan","Wu","Zan","Mou","Ao","Liu",
            "Bei","Xin","You","Fang","Ba","Ping","Nian","Lu","Su","Fu","Hou","Tai","Gui","Jie","Wei","Er",
            "Ji","Jiao","Xiang","Xun","Geng","Li","Lian","Jian","Shi","Tiao","Gun","Sha","Huan","Ji","Qing","Ling",
            "Zou","Fei","Kun","Chang","Gu","Ni","Nian","Diao","Shi","Zi","Fen","Die","E","Qiu","Fu","Huang",
            "Bian","Sao","Ao","Qi","Ta","Guan","Yao","Le","Biao","Xue","Man","Min","Yong","Gui","Shan","Zun",
            "Li","Da","Yang","Da","Qiao","Man","Jian","Ju","Rou","Gou","Bei","Jie","Tou","Ku","Gu","Di",
            "Hou","Ge","Ke","Bi","Lou","Qia","Kuan","Bin","Du","Mei","Ba","Yan","Liang","Xiao","Wang","Chi",
            "Xiang","Yan","Tie","Tao","Yong","Biao","Kun","Mao","Ran","Tiao","Ji","Zi","Xiu","Quan","Jiu","Bin",
            "Huan","Lie","Me","Hui","Mi","Ji","Jun","Zhu","Mi","Qi","Ao","She","Lin","Dai","Chu","You",
            "Xia","Yi","Qu","Du","Li","Qing","Can","An","Fen","You","Wu","Yan","Xi","Qiu","Han","Zha"
        };
        private const int FirstCode = -20319;
        private const int LastCode = -2050;
        private const int LevelOneLastCode = -10247;
    }
}