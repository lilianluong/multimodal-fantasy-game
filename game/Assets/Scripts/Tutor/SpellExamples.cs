using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellExamples
{
    private static Dictionary<string, List<GestureFrame>> templates = new Dictionary<string, List<GestureFrame>>()
    {
        { "shield", new List<GestureFrame>() {
            new GestureFrame(0, 0.23087374599732177f, 0.6280773051494786f, 0.03498915171836328f),
            new GestureFrame(9838, 0.24085763231724472f, 0.6274239841998765f, 0.034913638363167764f),
            new GestureFrame(19883, 0.2511484656218929f, 0.6265725035646832f, 0.03492604981585273f),
            new GestureFrame(29740, 0.26145198121158164f, 0.6255933040923445f, 0.03468609845783251f),
            new GestureFrame(39990, 0.27249311992145553f, 0.6244025214239797f, 0.03422864428230053f),
            new GestureFrame(49522, 0.28287130582427056f, 0.622141572193146f, 0.034502410994428474f),
            new GestureFrame(59138, 0.29388820402015564f, 0.6187736394901562f, 0.034709318768737625f),
            new GestureFrame(69167, 0.30509603699037274f, 0.6150119287601921f, 0.034798982605427464f),
            new GestureFrame(78629, 0.31573009634284765f, 0.6100477712435728f, 0.03453356282431767f),
            new GestureFrame(88816, 0.3271534874320836f, 0.6042093261565555f, 0.03433323240721483f),
            new GestureFrame(98511, 0.3381257980696438f, 0.5984656275946453f, 0.034523102179126096f),
            new GestureFrame(108157, 0.34924096157492446f, 0.5920826832202878f, 0.03491354876449375f),
            new GestureFrame(118255, 0.3605482979453053f, 0.5839909780977345f, 0.03531623575053233f),
            new GestureFrame(127994, 0.3717270927998066f, 0.5770060609593213f, 0.03577847327345677f),
            new GestureFrame(137724, 0.3830837247315652f, 0.5702155892341948f, 0.03655133921753348f),
            new GestureFrame(147736, 0.3931465355198386f, 0.5592687816373936f, 0.037878464595417924f),
            new GestureFrame(157850, 0.4036225753926821f, 0.5492032991795678f, 0.03939367184440027f),
            new GestureFrame(167461, 0.4135252814958196f, 0.5387942791148895f, 0.04048585117200849f),
            new GestureFrame(177071, 0.4228765321963794f, 0.5268379062405917f, 0.04152857406789237f),
            new GestureFrame(186983, 0.4320286783526036f, 0.5137047606724117f, 0.04279767615901627f),
            new GestureFrame(196787, 0.44100661210520115f, 0.5008047019820221f, 0.04425306203875723f),
            new GestureFrame(206838, 0.4498170126280534f, 0.4871864203207056f, 0.045773239977814066f),
            new GestureFrame(216511, 0.45766445547456613f, 0.47345071552442175f, 0.04753536691805502f),
            new GestureFrame(226300, 0.4649791120240329f, 0.45832619869952573f, 0.049068078489806856f),
            new GestureFrame(236273, 0.47225069604729913f, 0.44293471040739263f, 0.05061300908072787f),
            new GestureFrame(245862, 0.47902192849351377f, 0.42816480884359115f, 0.05207059827331626f),
            new GestureFrame(255929, 0.4847423476908475f, 0.41180578519740957f, 0.05344257695104737f),
            new GestureFrame(265540, 0.48955764108325317f, 0.3957955447531774f, 0.05487682772538762f),
            new GestureFrame(275503, 0.49396912139719457f, 0.37948555601768563f, 0.05634953160338277f),
            new GestureFrame(285232, 0.49811125180637034f, 0.36381689830014535f, 0.05802915343735457f),
            new GestureFrame(295452, 0.5017518251290624f, 0.3470631517682453f, 0.05976471437074925f),
            new GestureFrame(304914, 0.5040880861180751f, 0.3310959186875417f, 0.061532601589379475f),
            new GestureFrame(314857, 0.5062106624148892f, 0.3145278857708786f, 0.06350340167780359f),
            new GestureFrame(324922, 0.5077083612684351f, 0.2978643569583804f, 0.0653251044500106f),
            new GestureFrame(334530, 0.5083732160109833f, 0.28158290947323034f, 0.0667628965673966f),
            new GestureFrame(344509, 0.5086393729448202f, 0.2652711939269302f, 0.06844508685426663f),
            new GestureFrame(354258, 0.5085261528021977f, 0.24953176954758388f, 0.0707226342394621f),
            new GestureFrame(363910, 0.5071904157699401f, 0.23366980676351717f, 0.07399592070676998f),
            new GestureFrame(374023, 0.5054950785321789f, 0.21747831634708095f, 0.07626912408352571f),
            new GestureFrame(383605, 0.5038607172647538f, 0.20328963323629906f, 0.07748529473980684f),
            new GestureFrame(393762, 0.5011711279774189f, 0.18822793322722353f, 0.07912861587463367f),
            new GestureFrame(403517, 0.497722312107793f, 0.17432388066779422f, 0.08081310352750469f),
            new GestureFrame(413011, 0.4933163217489687f, 0.16074182945211699f, 0.08221532786671731f),
            new GestureFrame(423205, 0.48794338250014413f, 0.14703264586334244f, 0.08396186871034617f),
            new GestureFrame(433010, 0.4824237783672864f, 0.1344945332346488f, 0.08533849939195896f),
            new GestureFrame(442881, 0.4763258066483096f, 0.1229732352307134f, 0.08649099491784917f),
            new GestureFrame(452806, 0.46869960736394883f, 0.11118095825521211f, 0.08728621776742683f),
            new GestureFrame(462253, 0.4607724216975603f, 0.10052818092520145f, 0.08887115314813004f),
            new GestureFrame(472042, 0.4517341310200107f, 0.09009578375193861f, 0.090582921560906f),
            new GestureFrame(481941, 0.4423293491843922f, 0.0804835402592989f, 0.0912390852320745f),
            new GestureFrame(491704, 0.4321238158537425f, 0.07117949991461246f, 0.09215302632472576f),
            new GestureFrame(501557, 0.42147198338247605f, 0.06288676794889918f, 0.09254446256817461f),
            new GestureFrame(511395, 0.41045067856089607f, 0.054979261409458496f, 0.09348507352068068f),
            new GestureFrame(521243, 0.39927814746824203f, 0.04768436544027966f, 0.09430250055074677f),
            new GestureFrame(531105, 0.38757072858236963f, 0.040921815920110696f, 0.0946928290287714f),
            new GestureFrame(540926, 0.3757612649521249f, 0.034959040454892794f, 0.09487321152290426f),
            new GestureFrame(550814, 0.3642983201209771f, 0.025230351267365912f, 0.09532612060206334f),
            new GestureFrame(560780, 0.352421624903883f, 0.02111453028701534f, 0.0951564777308116f),
            new GestureFrame(570476, 0.3403195401496301f, 0.017012458630459505f, 0.09533086118645229f),
            new GestureFrame(580474, 0.32709251146174745f, 0.01297062986417965f, 0.0951529284015204f),
            new GestureFrame(590131, 0.31451231189226747f, 0.0076462042246687074f, 0.09502110024337118f),
            new GestureFrame(599966, 0.3018955316277862f, 0.003411526530566498f, 0.09452087288266318f),
            new GestureFrame(609797, 0.28819248962084887f, 0.0020220954565701625f, 0.09325138388817421f),
            new GestureFrame(619700, 0.27370993100462254f, 0.0006485152025409793f, 0.09213026418885824f),
            new GestureFrame(629403, 0.2593465936058444f, 0.0f, 0.09074044213882983f),
            new GestureFrame(639314, 0.24505193257253416f, 9.135806616216966e-05f, 0.08978389280401608f),
            new GestureFrame(649376, 0.2306805437656371f, 0.0006804612024953328f, 0.08715950080636363f),
            new GestureFrame(659176, 0.21611639715684183f, 0.0020738346181484527f, 0.08572958742237924f),
            new GestureFrame(668795, 0.20196512861962962f, 0.003826889692645107f, 0.08492762838158224f),
            new GestureFrame(678708, 0.18881271119120183f, 0.006546013961069897f, 0.08276643505626642f),
            new GestureFrame(689131, 0.1754324606254147f, 0.00914528778499485f, 0.08004577437421558f),
            new GestureFrame(698748, 0.16143449236494672f, 0.013353085876891714f, 0.07825659677975523f),
            new GestureFrame(708610, 0.14998206315999504f, 0.01963918222996992f, 0.07465002819018644f),
            new GestureFrame(718212, 0.1380326626916025f, 0.025968676922608753f, 0.06834727081094961f),
            new GestureFrame(727976, 0.1289350414011851f, 0.0317401184921286f, 0.0654450013483684f),
            new GestureFrame(737922, 0.11965312378364709f, 0.04048323855612747f, 0.06588915075716296f),
            new GestureFrame(747884, 0.1105363120863219f, 0.051586436565759716f, 0.06745470854736488f),
            new GestureFrame(757595, 0.10085964714706337f, 0.06057950434893229f, 0.06640056360233554f),
            new GestureFrame(767453, 0.09249834775972444f, 0.07262495459587194f, 0.06460455506351268f),
            new GestureFrame(776982, 0.08037308953023692f, 0.08727691172332998f, 0.0593969301409076f),
            new GestureFrame(786912, 0.07295324413747323f, 0.10018832500931786f, 0.057701663665735564f),
            new GestureFrame(796795, 0.06246845617591198f, 0.11520854897765714f, 0.053462405748507566f),
            new GestureFrame(806892, 0.055125486445455577f, 0.1292658641416609f, 0.05021966988362289f),
            new GestureFrame(816579, 0.0470203903938036f, 0.14329869443165583f, 0.04451320364343397f),
            new GestureFrame(826755, 0.03904405578204924f, 0.16075564393858371f, 0.04369908158263905f),
            new GestureFrame(836096, 0.033440668743650814f, 0.17436766998338693f, 0.039715048033767686f),
            new GestureFrame(845794, 0.027781746818029345f, 0.189034907757432f, 0.03762689188211089f),
            new GestureFrame(855629, 0.024321087632765075f, 0.2035061362140621f, 0.03519454649688163f),
            new GestureFrame(865538, 0.017915645845895136f, 0.2205335333870806f, 0.030989802905235377f),
            new GestureFrame(875410, 0.012976771446016873f, 0.23641634822619143f, 0.02718686456722203f),
            new GestureFrame(885191, 0.008078965820174778f, 0.2526042221143309f, 0.022872150821219905f),
            new GestureFrame(895071, 0.0047189666725025265f, 0.26892128099973617f, 0.01927862902038821f),
            new GestureFrame(905524, 0.0025820545878455295f, 0.285699007394937f, 0.016709672129617287f),
            new GestureFrame(915011, 0.0007813493094393979f, 0.30144524127350864f, 0.013948456847739125f),
            new GestureFrame(924862, 0.0f, 0.31730697598822327f, 0.010801930613581331f),
            new GestureFrame(934431, 0.000928616948188687f, 0.3316443928978748f, 0.008775982414028402f),
            new GestureFrame(944727, 0.0010068121546042389f, 0.34762882892398983f, 0.005830637784376621f),
            new GestureFrame(954354, 0.00039361512029428375f, 0.36375851254602176f, 0.003627080576918368f),
            new GestureFrame(964310, 0.002104607690006573f, 0.378713280610324f, 0.002792929139813469f),
            new GestureFrame(974258, 0.003535270444719109f, 0.3935703372479428f, 0.002124510813639531f),
            new GestureFrame(983945, 0.006240286994653094f, 0.40709080973856787f, 0.0005513739492376595f),
            new GestureFrame(993686, 0.010329423860814356f, 0.420018758052579f, 0.0f),
            new GestureFrame(1003592, 0.015009993428833248f, 0.43272222096150553f, 0.00028682979153304587f),
            new GestureFrame(1013516, 0.020275153618148405f, 0.4451269130192526f, 0.0010776561970833286f),
            new GestureFrame(1022989, 0.026058128980614544f, 0.45582916510798915f, 0.0018225225555294705f),
            new GestureFrame(1032802, 0.032389871785437827f, 0.46765619007834136f, 0.0019369726435862328f),
            new GestureFrame(1042914, 0.03965140446654603f, 0.47980642190187783f, 0.003045825469895766f),
            new GestureFrame(1053097, 0.04437397137667727f, 0.4941497034520105f, 0.006570938683114855f),
            new GestureFrame(1063015, 0.051121582354287265f, 0.5055673765167773f, 0.008680612770870434f),
            new GestureFrame(1072311, 0.05821800911518263f, 0.51614002005088f, 0.00988747025588746f),
            new GestureFrame(1082381, 0.06746151333756902f, 0.5250641783083989f, 0.010078433538888439f),
            new GestureFrame(1111612, 0.10337746763097212f, 0.5493028025797315f, 0.006578045487031268f),
            new GestureFrame(1121666, 0.11229738216947616f, 0.5528870753538044f, 0.004159919818635342f),
            new GestureFrame(1131611, 0.12240688201224677f, 0.556856915658178f, 0.003232948227914982f),
            new GestureFrame(1141154, 0.13295680820981898f, 0.5612513559333879f, 0.0023523480236658442f),
            new GestureFrame(1151182, 0.1446501356678686f, 0.565143261682034f, 0.0017342678616219182f),
            new GestureFrame(1160687, 0.1559004384095701f, 0.568507219462031f, 0.0016518167181905198f),
            new GestureFrame(1170716, 0.16806253391207995f, 0.5713904722105885f, 0.002007828904066325f),
            new GestureFrame(1180598, 0.18003780396126165f, 0.5739383326862951f, 0.00321926406679226f),
            new GestureFrame(1190552, 0.19207760541907115f, 0.5753695004517158f, 0.004150593411203486f),
            new GestureFrame(1200451, 0.20389206010272537f, 0.5762465248543382f, 0.005059087311741171f),
            new GestureFrame(1210125, 0.21567785950136187f, 0.5751200577432501f, 0.006006707324155638f)
        }},
        { "attack", new List<GestureFrame>() {
            new GestureFrame(0, 0.0f, 0.0f, 0.0001024462889576093f),
            new GestureFrame(9360, 0.02475518077667451f, 0.0011324888742661027f, 6.0338735682349835e-05f),
            new GestureFrame(18963, 0.050916120261780475f, 0.0027574375080342494f, 0.0f),
            new GestureFrame(28429, 0.07714043392816752f, 0.004814524250681745f, 0.0006000671602305007f),
            new GestureFrame(37755, 0.10381622599096364f, 0.0071011430747772285f, 0.0009464126420511657f),
            new GestureFrame(47099, 0.13089754902973605f, 0.009242607232041967f, 0.0018865379821492107f),
            new GestureFrame(56450, 0.15818357386325752f, 0.01138183894151176f, 0.002637742427599725f),
            new GestureFrame(65992, 0.18624979364095445f, 0.013863318565994905f, 0.0035655380496465864f),
            new GestureFrame(75408, 0.21377236682042233f, 0.01581917063516534f, 0.004143574025451682f),
            new GestureFrame(84690, 0.24108203737650663f, 0.018212719152622958f, 0.004813120428280074f),
            new GestureFrame(94199, 0.2694116518238882f, 0.020210714368942933f, 0.00533397270941915f),
            new GestureFrame(103495, 0.2971835871440439f, 0.0231546751682531f, 0.006344049978765549f),
            new GestureFrame(113321, 0.3260490659449816f, 0.02570991669028771f, 0.006998701057084533f),
            new GestureFrame(122689, 0.3534586214548302f, 0.028364752720072484f, 0.007856504884743846f),
            new GestureFrame(132123, 0.3816480430214559f, 0.03171078192326881f, 0.009144370986534398f),
            new GestureFrame(141213, 0.4090153827726514f, 0.034595514515774216f, 0.01055373179504316f),
            new GestureFrame(150839, 0.438070457328093f, 0.03843227219242752f, 0.011131955706504462f),
            new GestureFrame(160404, 0.4669198847154847f, 0.04271592946950229f, 0.01307043227000554f),
            new GestureFrame(169791, 0.49481173437434045f, 0.04654102374543097f, 0.01455186070764996f),
            new GestureFrame(179626, 0.5241427383382338f, 0.049953160739453224f, 0.016226854328658186f),
            new GestureFrame(188667, 0.5521072906209423f, 0.05341709963435011f, 0.0187470316132604f),
            new GestureFrame(197964, 0.5808385406506988f, 0.05566786261324917f, 0.02232324497768156f),
            new GestureFrame(207741, 0.6110959079391379f, 0.05841277107753626f, 0.026736905321182832f),
            new GestureFrame(216552, 0.638076998627933f, 0.060380924389658454f, 0.03167121862012721f),
            new GestureFrame(226039, 0.6671712606152038f, 0.06217818154506937f, 0.03742444730344567f),
            new GestureFrame(235907, 0.6958746619977223f, 0.06333604742794337f, 0.04180001081142575f),
            new GestureFrame(245384, 0.7230517980510411f, 0.0654007882773164f, 0.045652173516868184f),
            new GestureFrame(254475, 0.7490793601704387f, 0.06719658750763674f, 0.04920066637199874f),
            new GestureFrame(263919, 0.775625465240412f, 0.0685469450626262f, 0.053267554107187975f),
            new GestureFrame(273233, 0.8014630414964434f, 0.06941746302217894f, 0.05713053165539216f),
            new GestureFrame(282776, 0.8286774001997311f, 0.0714736841218039f, 0.06216430278160945f),
            new GestureFrame(292404, 0.8555454561338474f, 0.07375273995949201f, 0.06699131621089915f),
            new GestureFrame(301835, 0.8808558557891574f, 0.07524164595824552f, 0.07142619335098227f),
            new GestureFrame(311259, 0.9045947033171418f, 0.07568093701210053f, 0.07554179059124853f),
            new GestureFrame(320654, 0.9284898221907726f, 0.0770183554819111f, 0.07967459818160749f),
            new GestureFrame(330098, 0.9517641383367713f, 0.078864225327061f, 0.08343124970855735f),
            new GestureFrame(339267, 0.9743784700183287f, 0.08018806686946557f, 0.08721410971701833f),
            new GestureFrame(348960, 1.0f, 0.08218000258462782f, 0.09383378442067171f)
        }},
        { "heal", new List<GestureFrame>() {
            new GestureFrame(0, 0.0f, 0.4443588341517726f, 0.0f),
            new GestureFrame(9603, 0.005446281165334225f, 0.4373273867936138f, 0.0018237343218173447f),
            new GestureFrame(19149, 0.010711172331831338f, 0.4304250073054542f, 0.0025940142066281004f),
            new GestureFrame(47767, 0.03449662204563468f, 0.3981083415841033f, 0.002241701471377973f),
            new GestureFrame(57436, 0.04091170649977709f, 0.3902218692286894f, 0.00403779612158426f),
            new GestureFrame(67296, 0.049460311211568464f, 0.3805459204972273f, 0.006834196017303094f),
            new GestureFrame(77028, 0.05857264466632488f, 0.3699634898198994f, 0.010214424140082834f),
            new GestureFrame(86569, 0.06583329386835f, 0.3622166944297328f, 0.011516192394013402f),
            new GestureFrame(96195, 0.07639225121327699f, 0.348508268307593f, 0.01297458929504562f),
            new GestureFrame(105907, 0.08704642093307248f, 0.33522318479905006f, 0.014973723395016571f),
            new GestureFrame(115544, 0.0977027252368718f, 0.32209031627741147f, 0.017275965764165967f),
            new GestureFrame(125146, 0.10740261534905463f, 0.3112083630419913f, 0.0178038504714828f),
            new GestureFrame(134488, 0.11864864635249023f, 0.2970199179097144f, 0.019011201500815377f),
            new GestureFrame(144231, 0.13024130259492656f, 0.28227846659598005f, 0.02116459876482494f),
            new GestureFrame(153807, 0.14158771577967183f, 0.2675805012261377f, 0.023305796588029868f),
            new GestureFrame(163352, 0.152847015243809f, 0.25306833685596425f, 0.02654819223711793f),
            new GestureFrame(173258, 0.16431142624662648f, 0.23848911392652103f, 0.029643665701395015f),
            new GestureFrame(182870, 0.17523151123682607f, 0.22484207305068407f, 0.03320986236386726f),
            new GestureFrame(192279, 0.1858791495550683f, 0.21148241384319f, 0.03587062841628291f),
            new GestureFrame(202132, 0.1969566157927928f, 0.19804616491064156f, 0.03877509428656111f),
            new GestureFrame(211631, 0.2069913448362874f, 0.1855106962444271f, 0.041527211315207126f),
            new GestureFrame(221432, 0.21713851063227074f, 0.17303099624467813f, 0.04440440893858257f),
            new GestureFrame(230699, 0.22635657458468406f, 0.1623697348836739f, 0.04830602072821482f),
            new GestureFrame(240359, 0.2358499507861375f, 0.151125086750274f, 0.052519764014008485f),
            new GestureFrame(249992, 0.24488379218308529f, 0.14043540209130514f, 0.05693648425447866f),
            new GestureFrame(259854, 0.2538017295690278f, 0.1300980301675864f, 0.06001143052004948f),
            new GestureFrame(269449, 0.26227777615379194f, 0.12007569731292467f, 0.06349307531730024f),
            new GestureFrame(278995, 0.2700893253198355f, 0.11022486870925506f, 0.06591625011429952f),
            new GestureFrame(288653, 0.2774588666726208f, 0.1000239683289573f, 0.0678064703453591f),
            new GestureFrame(298303, 0.2844041434984521f, 0.09045575581073834f, 0.06933263762914747f),
            new GestureFrame(307762, 0.2909048878867549f, 0.08129077401164825f, 0.07067287271790454f),
            new GestureFrame(317640, 0.2974148017671405f, 0.07219920488235101f, 0.07239643228891755f),
            new GestureFrame(327392, 0.3032854609256881f, 0.06385476852095923f, 0.07505956694949692f),
            new GestureFrame(336632, 0.30862790628044395f, 0.0564931627146865f, 0.07679968904855293f),
            new GestureFrame(346576, 0.31435204503997777f, 0.048994886799038954f, 0.0785176887728583f),
            new GestureFrame(356206, 0.3195316734042222f, 0.04254811689197591f, 0.08147605909441925f),
            new GestureFrame(365968, 0.32389814582090787f, 0.036274666696054426f, 0.08341099921494113f),
            new GestureFrame(375524, 0.3276409154849262f, 0.030377371333126853f, 0.08495421125837471f),
            new GestureFrame(385202, 0.33109526810098516f, 0.02528578569448889f, 0.08618077443019691f),
            new GestureFrame(394833, 0.3341238669454362f, 0.020519068139623238f, 0.08732593974422521f),
            new GestureFrame(404194, 0.33647586648544403f, 0.017176451422449196f, 0.08809417723636785f),
            new GestureFrame(414088, 0.33870779035650544f, 0.014344404505739485f, 0.08871297966495995f),
            new GestureFrame(423781, 0.34083453100515887f, 0.011335222574917906f, 0.0893629143984584f),
            new GestureFrame(433071, 0.34307053966155654f, 0.007148558721079809f, 0.09026433291250116f),
            new GestureFrame(442760, 0.34480220497847114f, 0.0024422761325950254f, 0.08987486706895845f),
            new GestureFrame(452394, 0.3458052467110351f, 2.704751957012854e-05f, 0.08966969170550723f),
            new GestureFrame(462163, 0.34686605695288547f, 0.0f, 0.0897177943078261f),
            new GestureFrame(471543, 0.34793909318417965f, 0.0006068601047965861f, 0.08967623019870169f),
            new GestureFrame(481289, 0.3487900333960986f, 0.0011349178924816704f, 0.0894662183359461f),
            new GestureFrame(491006, 0.3506912173044669f, 0.004020847099095374f, 0.08918237539656634f),
            new GestureFrame(500451, 0.3527127889139947f, 0.008599253213304186f, 0.08900719058378688f),
            new GestureFrame(510288, 0.3552127200896168f, 0.017784077424496326f, 0.08876172051498761f),
            new GestureFrame(519855, 0.357717275015307f, 0.02700702629882664f, 0.08824147062427334f),
            new GestureFrame(529209, 0.3595867734707546f, 0.03787600183083483f, 0.08775763631116595f),
            new GestureFrame(538831, 0.3608867847705757f, 0.04915121493784439f, 0.08688070523421698f),
            new GestureFrame(548458, 0.3615055871991678f, 0.06125976680329281f, 0.08552901005326659f),
            new GestureFrame(558072, 0.36211769511845887f, 0.07413337910562236f, 0.08434792557651603f),
            new GestureFrame(567730, 0.36252779054627776f, 0.08841991581909853f, 0.08290766998015092f),
            new GestureFrame(577357, 0.3626015577981311f, 0.10384993712763653f, 0.0814428276637151f),
            new GestureFrame(587183, 0.3629196320896254f, 0.12069598898617061f, 0.08000247278437307f),
            new GestureFrame(596778, 0.3630661879468452f, 0.13834413383197783f, 0.07893793233924692f),
            new GestureFrame(606407, 0.3632770933563931f, 0.15669412422493148f, 0.0779019818456538f),
            new GestureFrame(616101, 0.3633921906932109f, 0.17538493889437712f, 0.07658319542573257f),
            new GestureFrame(625539, 0.363684706709789f, 0.19452839883889408f, 0.07520560157108787f),
            new GestureFrame(635347, 0.36362374696195865f, 0.21477653791777868f, 0.074429712198081f),
            new GestureFrame(645050, 0.3636292217089718f, 0.23532258266055997f, 0.07415150002182098f),
            new GestureFrame(654532, 0.36365961648319245f, 0.2559007099881696f, 0.07373078840711257f),
            new GestureFrame(664316, 0.36405574137783014f, 0.2775228120014f, 0.0733006910052644f),
            new GestureFrame(673847, 0.364151677100102f, 0.2984406856760379f, 0.07314569609500621f),
            new GestureFrame(683650, 0.3642021128523785f, 0.31965374182434825f, 0.07331981006996316f),
            new GestureFrame(693270, 0.3648063632332103f, 0.34083119793379074f, 0.07456409170734539f),
            new GestureFrame(702637, 0.3652076933924952f, 0.3614861986521031f, 0.07518967374493304f),
            new GestureFrame(712201, 0.3652898287809748f, 0.38165737924059046f, 0.07540277756325563f),
            new GestureFrame(721843, 0.3653764035368511f, 0.4028242829879144f, 0.07522259668924461f),
            new GestureFrame(731486, 0.3657729681132438f, 0.42262023065676496f, 0.07589843009643739f),
            new GestureFrame(741259, 0.3654567376484637f, 0.44122336427430575f, 0.07702865332243887f),
            new GestureFrame(750925, 0.36564788574560403f, 0.4615842604484093f, 0.07859453379699619f),
            new GestureFrame(760685, 0.36513986893597483f, 0.47972552965730736f, 0.0797696683867726f),
            new GestureFrame(770199, 0.3643136076354623f, 0.4969575937301155f, 0.0805849411892322f),
            new GestureFrame(779642, 0.3636117053551862f, 0.5135238945262472f, 0.07910989400109703f),
            new GestureFrame(789351, 0.3624386060664367f, 0.5261347047886633f, 0.08040612900197447f),
            new GestureFrame(798934, 0.36120039132810855f, 0.5396965325037362f, 0.08088124059639511f),
            new GestureFrame(808685, 0.3598811758638942f, 0.5523472261563102f, 0.08234040959986393f),
            new GestureFrame(818215, 0.3587213237609226f, 0.565611715538783f, 0.08235894714998357f),
            new GestureFrame(827718, 0.3572425038196643f, 0.5757169071293277f, 0.08245372693473836f),
            new GestureFrame(837584, 0.35610270687803103f, 0.5845466244986334f, 0.08386273675993777f),
            new GestureFrame(847131, 0.3548142549533802f, 0.5919042306192643f, 0.08484192930311676f),
            new GestureFrame(856943, 0.3538080219822723f, 0.5978081070252337f, 0.08613794800893734f),
            new GestureFrame(866208, 0.3529320624601689f, 0.6023435236104351f, 0.08799790111531801f),
            new GestureFrame(875839, 0.35185373586453295f, 0.6073244654629343f, 0.08927305621270248f),
            new GestureFrame(885480, 0.35097898192143506f, 0.6122330158419748f, 0.0871004042788637f),
            new GestureFrame(895226, 0.349542158679415f, 0.6152592177116973f, 0.08649348034929623f),
            new GestureFrame(904975, 0.34837712967836104f, 0.6165576121173229f, 0.0863789503437751f),
            new GestureFrame(914715, 0.34718488295834793f, 0.6188121866904049f, 0.08616907322220249f),
            new GestureFrame(924207, 0.34588986417393774f, 0.6201559108666367f, 0.08596120303509196f),
            new GestureFrame(933952, 0.34469775928674884f, 0.6203160117585652f, 0.0860717759048186f),
            new GestureFrame(943629, 0.34271265289631636f, 0.6193521726186031f, 0.08638808437765197f),
            new GestureFrame(953273, 0.34086781916899267f, 0.618195667770282f, 0.08635339916050003f),
            new GestureFrame(962479, 0.33840069392560224f, 0.6154047381893006f, 0.084847645165931f),
            new GestureFrame(972272, 0.3352384176443664f, 0.6108386857140774f, 0.08334097634964605f),
            new GestureFrame(982152, 0.33156483403198056f, 0.6043214674432468f, 0.08198339510649241f),
            new GestureFrame(991644, 0.32822533763693834f, 0.5972634122472729f, 0.08088157390353193f),
            new GestureFrame(1001326, 0.3245359467562982f, 0.589750073685698f, 0.0794488567217717f),
            new GestureFrame(1010835, 0.3203648419632721f, 0.5807103870034839f, 0.0774985561063917f),
            new GestureFrame(1020256, 0.31566738137645095f, 0.5708685506008669f, 0.07754596727369271f),
            new GestureFrame(1029940, 0.31071646553282956f, 0.5598260142419161f, 0.0761798086904971f),
            new GestureFrame(1039672, 0.3057550611518604f, 0.5494655235678569f, 0.07492157070321608f),
            new GestureFrame(1049385, 0.30038814254329316f, 0.5386597912921482f, 0.07264808981467144f),
            new GestureFrame(1059095, 0.2945423892286707f, 0.526314548809717f, 0.071140045219992f),
            new GestureFrame(1068584, 0.28859988465303743f, 0.5143908334807331f, 0.06961391339440967f),
            new GestureFrame(1078131, 0.2818998722384103f, 0.5007022072208481f, 0.06861973266753602f),
            new GestureFrame(1088110, 0.2750978217442513f, 0.48665202072557673f, 0.06762017293080554f),
            new GestureFrame(1097849, 0.2677920980707034f, 0.47305422420629323f, 0.06856722970184712f),
            new GestureFrame(1107500, 0.2600528694617066f, 0.45859533224487986f, 0.06767466155603288f),
            new GestureFrame(1117069, 0.25172837124957315f, 0.4435756332966795f, 0.0668502298989522f),
            new GestureFrame(1126557, 0.24340445455201884f, 0.42817179432748415f, 0.0659924320699844f),
            new GestureFrame(1136182, 0.23468614686610234f, 0.4114669512286985f, 0.06521389042316546f),
            new GestureFrame(1146011, 0.226020384695722f, 0.39372445918825016f, 0.06441306329384806f),
            new GestureFrame(1155594, 0.21717617577921997f, 0.3766102298257665f, 0.06402702271300918f),
            new GestureFrame(1165031, 0.20841590840356788f, 0.3593676701239694f, 0.06348753674551622f),
            new GestureFrame(1174597, 0.1992344420844881f, 0.3419456067998968f, 0.06355490733699923f),
            new GestureFrame(1184263, 0.1901128824045191f, 0.3244764549781981f, 0.0638338180399183f),
            new GestureFrame(1193849, 0.18115486838119574f, 0.3069017795353134f, 0.06390348632315296f),
            new GestureFrame(1203431, 0.17213800614621197f, 0.28890506634081425f, 0.06405476875923836f),
            new GestureFrame(1212817, 0.1634198048349136f, 0.27130829334392315f, 0.06439788363584076f),
            new GestureFrame(1222675, 0.15468165119107447f, 0.25302133345803274f, 0.06504762689502659f),
            new GestureFrame(1232334, 0.1460610663210144f, 0.23546302552305784f, 0.06612580456619714f),
            new GestureFrame(1242034, 0.13770464508597985f, 0.21835849752574765f, 0.06793667287795153f),
            new GestureFrame(1270962, 0.1119404218173434f, 0.1607533796528134f, 0.06956133596648809f),
            new GestureFrame(1280544, 0.10719696484564241f, 0.1494378151073261f, 0.07109373325707834f),
            new GestureFrame(1289845, 0.10177237701076572f, 0.1375704641426693f, 0.0725520512636021f),
            new GestureFrame(1299522, 0.09583614144596096f, 0.12467604496503332f, 0.07397525500868081f),
            new GestureFrame(1309273, 0.08991249354430171f, 0.11242398664808487f, 0.07517620317245705f),
            new GestureFrame(1319001, 0.08390523519279144f, 0.10095907213874154f, 0.0766332127711778f),
            new GestureFrame(1328365, 0.07864428697880639f, 0.09019600317654466f, 0.07775629471448209f),
            new GestureFrame(1338186, 0.07308951788626088f, 0.0796772270707064f, 0.07890995581467845f),
            new GestureFrame(1347637, 0.06791027956228292f, 0.0701515076666102f, 0.08033493956170054f),
            new GestureFrame(1357507, 0.06361579439550716f, 0.06024264107128828f, 0.08108167874351889f),
            new GestureFrame(1367153, 0.05986696846989657f, 0.051396499460943074f, 0.08217862089732778f)
        }}
    };

    public static List<GestureFrame> GetExample(string gestureName)
    {
        return templates[gestureName];
    }

    public static List<string> GetExampleNames()
    {
        return new List<string>(templates.Keys);
    }
}


public struct GestureFrame
{
    public GestureFrame(int timestamp, float x, float y, float z)
    {
        this.timestamp = timestamp;
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public int timestamp { get; }
    public float x { get; }
    public float y { get; }
    public float z { get; }

    public override string ToString() => $"({timestamp}, {x}, {y}, {z})";
}
