using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrestSharp;
using CrestSharp.Implementation;
using CrestSharp.Infrastructure;
using CrestSharp.Model;
using CrestSharp.Model.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace CrestSharpTests
{
    [TestClass]
    public class PageTest
    {

        [TestMethod]
        public void TestDeserialization()
        {
            var settings = new JsonSerializerSettings();
            settings.ContractResolver = new CrestJsonContractResolver();
           var x= JsonConvert.DeserializeObject<Page<CrestGroup>>(JSON, settings);

            Console.Write("ab");
        }

        private const string JSON = @"{
totalCount_str: ""1215"",
pageCount: 2,
items: [
{
href: ""https://public-crest.eveonline.com/inventory/groups/0/"",
name: ""#System""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1/"",
name: ""Character""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/2/"",
name: ""Corporation""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/3/"",
name: ""Region""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/4/"",
name: ""Constellation""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/5/"",
name: ""Solar System""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/6/"",
name: ""Sun""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/7/"",
name: ""Planet""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/8/"",
name: ""Moon""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/9/"",
name: ""Asteroid Belt""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/10/"",
name: ""Stargate""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/11/"",
name: ""Asteroid OLD""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/12/"",
name: ""Cargo Container""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/13/"",
name: ""Ring""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/14/"",
name: ""Biomass""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/15/"",
name: ""Station""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/16/"",
name: ""Station Services""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/17/"",
name: ""Money""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/18/"",
name: ""Mineral""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/19/"",
name: ""Faction""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/20/"",
name: ""Drug""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/23/"",
name: ""Clone""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/24/"",
name: ""Voucher""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/25/"",
name: ""Frigate""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/26/"",
name: ""Cruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/27/"",
name: ""Battleship""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/28/"",
name: ""Industrial""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/29/"",
name: ""Capsule""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/30/"",
name: ""Titan""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/31/"",
name: ""Shuttle""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/32/"",
name: ""Alliance""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/38/"",
name: ""Shield Extender""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/39/"",
name: ""Shield Recharger""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/40/"",
name: ""Shield Booster""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/41/"",
name: ""Remote Shield Booster""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/43/"",
name: ""Capacitor Recharger""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/46/"",
name: ""Propulsion Module""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/47/"",
name: ""Cargo Scanner""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/48/"",
name: ""Ship Scanner""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/49/"",
name: ""Survey Scanner""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/52/"",
name: ""Warp Scrambler""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/53/"",
name: ""Energy Weapon""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/54/"",
name: ""Mining Laser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/55/"",
name: ""Projectile Weapon""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/56/"",
name: ""Missile Launcher""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/57/"",
name: ""Shield Power Relay""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/59/"",
name: ""Gyrostabilizer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/60/"",
name: ""Damage Control""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/61/"",
name: ""Capacitor Battery""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/62/"",
name: ""Armor Repair Unit""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/63/"",
name: ""Hull Repair Unit""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/65/"",
name: ""Stasis Web""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/67/"",
name: ""Remote Capacitor Transmitter""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/68/"",
name: ""Energy Nosferatu""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/71/"",
name: ""Energy Neutralizer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/72/"",
name: ""Smart Bomb""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/74/"",
name: ""Hybrid Weapon""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/76/"",
name: ""Capacitor Booster""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/77/"",
name: ""Shield Hardener""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/78/"",
name: ""Reinforced Bulkhead""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/80/"",
name: ""ECM Burst""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/82/"",
name: ""Passive Targeting System""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/83/"",
name: ""Projectile Ammo""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/85/"",
name: ""Hybrid Charge""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/368726/"",
name: ""Infantry Color Skin""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/87/"",
name: ""Capacitor Booster Charge""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/88/"",
name: ""Light Defender Missile""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/89/"",
name: ""Torpedo""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/90/"",
name: ""Bomb""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/92/"",
name: ""Mine""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/94/"",
name: ""Trading""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/95/"",
name: ""Trade Session""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/96/"",
name: ""Automated Targeting System""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/368656/"",
name: ""Battle Salvage""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/98/"",
name: ""Armor Coating""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/99/"",
name: ""Sentry Gun""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/100/"",
name: ""Combat Drone""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/101/"",
name: ""Mining Drone""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/104/"",
name: ""Clone Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/105/"",
name: ""Frigate Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/106/"",
name: ""Cruiser Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/107/"",
name: ""Battleship Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/108/"",
name: ""Industrial Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/109/"",
name: ""Capsule Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/110/"",
name: ""Titan Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/111/"",
name: ""Shuttle Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/118/"",
name: ""Shield Extender Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/119/"",
name: ""Shield Recharger Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/120/"",
name: ""Shield Booster Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/121/"",
name: ""Remote Shield Booster Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/123/"",
name: ""Capacitor Recharger Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/126/"",
name: ""Propulsion Module Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/127/"",
name: ""Cargo Scanner Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/128/"",
name: ""Ship Scanner Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/129/"",
name: ""Survey Scanner Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/130/"",
name: ""ECM Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/131/"",
name: ""ECCM Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/132/"",
name: ""Warp Scrambler Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/133/"",
name: ""Energy Weapon Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/134/"",
name: ""Mining Laser Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/135/"",
name: ""Projectile Weapon Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/136/"",
name: ""Missile Launcher Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/137/"",
name: ""Power Manager Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/139/"",
name: ""Fast Loader Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/140/"",
name: ""Damage Control Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/141/"",
name: ""Capacitor Battery Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/142/"",
name: ""Armor Repair Unit Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/143/"",
name: ""Hull Repair Unit Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/145/"",
name: ""Stasis Web Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/147/"",
name: ""Remote Capacitor Transmitter Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/148/"",
name: ""Energy Nosferatu Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/151/"",
name: ""Energy Neutralizer Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/152/"",
name: ""Smart Bomb Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/154/"",
name: ""Hybrid Weapon Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/156/"",
name: ""Capacitor Booster Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/157/"",
name: ""Shield Hardener Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/158/"",
name: ""Hull Mods Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/160/"",
name: ""ECM Burst Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/161/"",
name: ""Passive Targeting System Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/162/"",
name: ""Automated Targeting System Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/163/"",
name: ""Armor Coating Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/165/"",
name: ""Projectile Ammo Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/166/"",
name: ""Missile Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/167/"",
name: ""Hybrid Charge Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/168/"",
name: ""Frequency Crystal Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/169/"",
name: ""Capacitor Booster Charge Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/170/"",
name: ""Defender Missile Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/172/"",
name: ""Bomb Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/174/"",
name: ""Mine Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/175/"",
name: ""Proximity Drone Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/176/"",
name: ""Combat Drone Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/177/"",
name: ""Mining Drone Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/178/"",
name: ""Drug Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/180/"",
name: ""Protective Sentry Gun""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/182/"",
name: ""Police Drone""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/185/"",
name: ""Pirate Drone""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/186/"",
name: ""Wreck""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/190/"",
name: ""Bloodline Bonus""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/191/"",
name: ""Physical Benefit""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/192/"",
name: ""Physical Handicap""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/193/"",
name: ""Phobia Handicap""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/194/"",
name: ""Social Handicap""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/195/"",
name: ""Amarr Education""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/196/"",
name: ""Caldari Education""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/197/"",
name: ""Gallente Education""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/198/"",
name: ""Minmatar Education""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/199/"",
name: ""Career Bonus""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/201/"",
name: ""ECM""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/202/"",
name: ""ECCM""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/203/"",
name: ""Sensor Backup Array""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/205/"",
name: ""Heat Sink""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/208/"",
name: ""Remote Sensor Damper""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/209/"",
name: ""Remote Tracking Computer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/210/"",
name: ""Signal Amplifier""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/211/"",
name: ""Tracking Enhancer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/212/"",
name: ""Sensor Booster""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/213/"",
name: ""Tracking Computer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/218/"",
name: ""Heat Sink Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/223/"",
name: ""Sensor Booster Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/224/"",
name: ""Tracking Computer Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/225/"",
name: ""Cheat Module Group""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/226/"",
name: ""Large Collidable Object""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/227/"",
name: ""Cloud""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/237/"",
name: ""Rookie ship""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1440/"",
name: ""Structure Drone Damage Amplifier""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/255/"",
name: ""Gunnery""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/256/"",
name: ""Missiles""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/257/"",
name: ""Spaceship Command""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/258/"",
name: ""Leadership""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/266/"",
name: ""Corporation Management""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/267/"",
name: ""Obsolete Books""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/268/"",
name: ""Production""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/269/"",
name: ""Rigging""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/270/"",
name: ""Science""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/272/"",
name: ""Electronic Systems""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/273/"",
name: ""Drones""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/274/"",
name: ""Trade""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/275/"",
name: ""Navigation""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/278/"",
name: ""Social""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/279/"",
name: ""LCO Drone""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/280/"",
name: ""General""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/281/"",
name: ""Frozen""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/282/"",
name: ""Radioactive""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/283/"",
name: ""Livestock""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/284/"",
name: ""Biohazard""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/285/"",
name: ""CPU Enhancer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/286/"",
name: ""Minor Threat""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/287/"",
name: ""Rogue Drone""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/288/"",
name: ""Faction Drone""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/289/"",
name: ""Projected ECCM""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/290/"",
name: ""Remote Sensor Booster""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/291/"",
name: ""Weapon Disruptor""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/295/"",
name: ""Shield Amplifier""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/296/"",
name: ""Shield Amplifier Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/297/"",
name: ""Convoy""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/298/"",
name: ""Convoy Drone""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/299/"",
name: ""Repair Drone""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/300/"",
name: ""Cyberimplant""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/301/"",
name: ""Concord Drone""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/302/"",
name: ""Magnetic Field Stabilizer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/303/"",
name: ""Booster""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/304/"",
name: ""DNA Mutator""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/305/"",
name: ""Comet""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/306/"",
name: ""Spawn Container""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/307/"",
name: ""Construction Platform""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/308/"",
name: ""Countermeasure Launcher""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/309/"",
name: ""Autopilot""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/310/"",
name: ""Beacon""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/311/"",
name: ""Reprocessing Array""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/312/"",
name: ""Planetary Cloud""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/313/"",
name: ""Drugs""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/314/"",
name: ""Miscellaneous""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/315/"",
name: ""Warp Core Stabilizer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/316/"",
name: ""Gang Coordinator""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/317/"",
name: ""Computer Interface Node""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/318/"",
name: ""Landmark""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/319/"",
name: ""Large Collidable Structure""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/321/"",
name: ""Shield Disruptor""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/323/"",
name: ""Billboard""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/324/"",
name: ""Assault Frigate""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/325/"",
name: ""Remote Armor Repairer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/326/"",
name: ""Armor Plating Energized""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/328/"",
name: ""Armor Hardener""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/329/"",
name: ""Armor Reinforcer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/330/"",
name: ""Cloaking Device""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/332/"",
name: ""Tool""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/333/"",
name: ""Datacores""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/334/"",
name: ""Construction Components""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/335/"",
name: ""Temporary Cloud""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/336/"",
name: ""Mobile Sentry Gun""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/337/"",
name: ""Mission Drone""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/338/"",
name: ""Shield Boost Amplifier""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/339/"",
name: ""Auxiliary Power Core""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/340/"",
name: ""Secure Cargo Container""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/341/"",
name: ""Signature Scrambling""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/342/"",
name: ""Anti Warp Scrambler Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/343/"",
name: ""Weapon Disruptor Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/344/"",
name: ""Tracking Enhancer Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/345/"",
name: ""Remote Tracking Computer Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/346/"",
name: ""Co-Processor Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/347/"",
name: ""Signal Amplifier Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/348/"",
name: ""Armor Hardener Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/349/"",
name: ""Armor Reinforcer Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/350/"",
name: ""Remote Armor Repairer Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/352/"",
name: ""Auxiliary Power Core Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/353/"",
name: ""QA Module""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/355/"",
name: ""Refinables""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/356/"",
name: ""Tool Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/357/"",
name: ""DroneBayExpander""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/358/"",
name: ""Heavy Assault Cruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/360/"",
name: ""Shield Boost Amplifier Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/361/"",
name: ""Mobile Warp Disruptor""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/363/"",
name: ""Ship Maintenance Array""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/364/"",
name: ""Mobile Storage""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/365/"",
name: ""Control Tower""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/366/"",
name: ""Warp Gate""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/367/"",
name: ""Ballistic Control system""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/368/"",
name: ""Global Warp Disruptor""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/369/"",
name: ""Ship Logs""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/370/"",
name: ""Criminal Tags""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/371/"",
name: ""Mobile Warp Disruptor Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/372/"",
name: ""Advanced Autocannon Ammo""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/373/"",
name: ""Advanced Railgun Charge""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/374/"",
name: ""Advanced Beam Laser Crystal""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/375/"",
name: ""Advanced Pulse Laser Crystal""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/376/"",
name: ""Advanced Artillery Ammo""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/377/"",
name: ""Advanced Blaster Charge""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/378/"",
name: ""Cruise Control""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/379/"",
name: ""Target Painter""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/380/"",
name: ""Deep Space Transport""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/381/"",
name: ""Elite Battleship""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/382/"",
name: ""Shipping Crates""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/383/"",
name: ""Destructible Sentry Gun""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/384/"",
name: ""Light Missile""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/385/"",
name: ""Heavy Missile""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/386/"",
name: ""Cruise Missile""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/387/"",
name: ""Rocket""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/394/"",
name: ""FoF Light Missile""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/395/"",
name: ""FoF Heavy Missile""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/396/"",
name: ""FoF Cruise Missile""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/397/"",
name: ""Assembly Array""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/400/"",
name: ""Ballistic Control System Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/401/"",
name: ""Cloaking Device Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/404/"",
name: ""Silo""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/405/"",
name: ""Anti Cloaking Pulse""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/406/"",
name: ""Smartbomb Supercharger""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/407/"",
name: ""Drone Control Unit""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/408/"",
name: ""Drone Control Unit Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/409/"",
name: ""Empire Insignia Drops""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/410/"",
name: ""Anti Cloaking Pulse Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/411/"",
name: ""Force Field""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/413/"",
name: ""Laboratory""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/414/"",
name: ""Mobile Power Core""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/416/"",
name: ""Moon Mining""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/417/"",
name: ""Mobile Missile Sentry""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/418/"",
name: ""Mobile Shield Generator""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/419/"",
name: ""Combat Battlecruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/420/"",
name: ""Destroyer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/422/"",
name: ""Gas Isotopes""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/423/"",
name: ""Ice Product""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/425/"",
name: ""Orbital Assault Unit""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/426/"",
name: ""Mobile Projectile Sentry""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/427/"",
name: ""Moon Materials""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/428/"",
name: ""Intermediate Materials""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/429/"",
name: ""Composite""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/430/"",
name: ""Mobile Laser Sentry""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/435/"",
name: ""Deadspace Overseer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/436/"",
name: ""Simple Reaction""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/438/"",
name: ""Mobile Reactor""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/439/"",
name: ""Electronic Warfare Battery""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/440/"",
name: ""Sensor Dampening Battery""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/441/"",
name: ""Stasis Webification Battery""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/443/"",
name: ""Warp Scrambling Battery""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/444/"",
name: ""Shield Hardening Array""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/445/"",
name: ""Force Field Array""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/446/"",
name: ""Customs Official""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/447/"",
name: ""Construction Component Blueprints""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/448/"",
name: ""Audit Log Secure Container""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/354753/"",
name: ""Infantry Installations""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/450/"",
name: ""Arkonor""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/368666/"",
name: ""Warbarge""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/452/"",
name: ""Crokite""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/453/"",
name: ""Dark Ochre""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/454/"",
name: ""Hedbergite""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/455/"",
name: ""Hemorphite""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/456/"",
name: ""Jaspet""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/457/"",
name: ""Kernite""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/458/"",
name: ""Plagioclase""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/459/"",
name: ""Pyroxeres""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/460/"",
name: ""Scordite""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/461/"",
name: ""Spodumain""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/462/"",
name: ""Veldspar""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/463/"",
name: ""Mining Barge""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/464/"",
name: ""Strip Miner""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/465/"",
name: ""Ice""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/467/"",
name: ""Gneiss""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/468/"",
name: ""Mercoxit""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/469/"",
name: ""Omber""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/470/"",
name: ""Unanchoring Drone""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/471/"",
name: ""Corporate Hangar Array""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/472/"",
name: ""System Scanner""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/473/"",
name: ""Tracking Array""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/474/"",
name: ""Acceleration Gate Keys""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/475/"",
name: ""Microwarpdrive""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/476/"",
name: ""Citadel Torpedo""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/477/"",
name: ""Mining Barge Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/478/"",
name: ""System Scanner Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/479/"",
name: ""Scanner Probe""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/480/"",
name: ""Stealth Emitter Array""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/481/"",
name: ""Scan Probe Launcher""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/482/"",
name: ""Mining Crystal""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/483/"",
name: ""Frequency Mining Laser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/484/"",
name: ""Complex Reactions""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/485/"",
name: ""Dreadnought""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/486/"",
name: ""Scan Probe Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/487/"",
name: ""Destroyer Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/489/"",
name: ""Battlecruiser Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/490/"",
name: ""Strip Miner Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/492/"",
name: ""Survey Probe""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/493/"",
name: ""Overseer Personal Effects""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/494/"",
name: ""Deadspace Overseer's Structure""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/495/"",
name: ""Deadspace Overseer's Sentry""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/496/"",
name: ""Deadspace Overseer's Belongings""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/497/"",
name: ""Fuel""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/498/"",
name: ""Modifications""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/499/"",
name: ""New EW Testing""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/500/"",
name: ""Festival Charges""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/501/"",
name: ""Festival Launcher""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/502/"",
name: ""Cosmic Signature""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/503/"",
name: ""Elite Industrial Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/504/"",
name: ""Target Painter Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/505/"",
name: ""Fake Skills""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/506/"",
name: ""Missile Launcher Cruise""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/507/"",
name: ""Missile Launcher Rocket""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/508/"",
name: ""Missile Launcher Torpedo""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/509/"",
name: ""Missile Launcher Light""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/510/"",
name: ""Missile Launcher Heavy""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/511/"",
name: ""Missile Launcher Rapid Light""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/512/"",
name: ""Missile Launcher Defender""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/513/"",
name: ""Freighter""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/514/"",
name: ""ECM Stabilizer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/515/"",
name: ""Siege Module""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/516/"",
name: ""Siege Module Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/517/"",
name: ""Agents in Space""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/518/"",
name: ""Anti Ballistic Defense System""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/519/"",
name: ""Terran Artifacts""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/520/"",
name: ""Storyline Frigate""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/521/"",
name: ""Identification""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/522/"",
name: ""Storyline Cruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/523/"",
name: ""Storyline Battleship""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/524/"",
name: ""Missile Launcher Citadel""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/525/"",
name: ""Freighter Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/526/"",
name: ""Commodities""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/527/"",
name: ""Storyline Mission Frigate""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/528/"",
name: ""Artifacts and Prototypes""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/530/"",
name: ""Materials and Compounds""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/532/"",
name: ""Gang Coordinator Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/533/"",
name: ""Storyline Mission Cruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/534/"",
name: ""Storyline Mission Battleship""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/535/"",
name: ""Construction Platform Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/536/"",
name: ""Station Components""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/537/"",
name: ""Dreadnought Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/538/"",
name: ""Data Miners""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/540/"",
name: ""Command Ship""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/541/"",
name: ""Interdictor""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/543/"",
name: ""Exhumer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/544/"",
name: ""Energy Neutralizer Drone""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/545/"",
name: ""Warp Scrambling Drone""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/546/"",
name: ""Mining Upgrade""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/547/"",
name: ""Carrier""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/548/"",
name: ""Interdiction Probe""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/549/"",
name: ""Fighter Drone""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/550/"",
name: ""Asteroid Angel Cartel Frigate""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/551/"",
name: ""Asteroid Angel Cartel Cruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/552/"",
name: ""Asteroid Angel Cartel Battleship""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/553/"",
name: ""Asteroid Angel Cartel Officer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/554/"",
name: ""Asteroid Angel Cartel Hauler""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/555/"",
name: ""Asteroid Blood Raiders Cruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/556/"",
name: ""Asteroid Blood Raiders Battleship""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/557/"",
name: ""Asteroid Blood Raiders Frigate""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/558/"",
name: ""Asteroid Blood Raiders Hauler""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/559/"",
name: ""Asteroid Blood Raiders Officer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/560/"",
name: ""Asteroid Guristas Battleship""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/561/"",
name: ""Asteroid Guristas Cruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/562/"",
name: ""Asteroid Guristas Frigate""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/563/"",
name: ""Asteroid Guristas Hauler""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/564/"",
name: ""Asteroid Guristas Officer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/565/"",
name: ""Asteroid Sansha's Nation Battleship""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/566/"",
name: ""Asteroid Sansha's Nation Cruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/567/"",
name: ""Asteroid Sansha's Nation Frigate""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/568/"",
name: ""Asteroid Sansha's Nation Hauler""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/569/"",
name: ""Asteroid Sansha's Nation Officer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/570/"",
name: ""Asteroid Serpentis Battleship""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/571/"",
name: ""Asteroid Serpentis Cruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/572/"",
name: ""Asteroid Serpentis Frigate""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/573/"",
name: ""Asteroid Serpentis Hauler""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/574/"",
name: ""Asteroid Serpentis Officer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/575/"",
name: ""Asteroid Angel Cartel Destroyer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/576/"",
name: ""Asteroid Angel Cartel BattleCruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/577/"",
name: ""Asteroid Blood Raiders Destroyer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/578/"",
name: ""Asteroid Blood Raiders BattleCruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/579/"",
name: ""Asteroid Guristas Destroyer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/580/"",
name: ""Asteroid Guristas BattleCruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/581/"",
name: ""Asteroid Sansha's Nation Destroyer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/582/"",
name: ""Asteroid Sansha's Nation BattleCruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/97/"",
name: ""Proximity Drone""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/584/"",
name: ""Asteroid Serpentis BattleCruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/585/"",
name: ""Remote Hull Repairer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/586/"",
name: ""Drone Modules""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/588/"",
name: ""Super Weapon""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/589/"",
name: ""Interdiction Sphere Launcher""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/590/"",
name: ""Jump Portal Generator""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/593/"",
name: ""Deadspace Angel Cartel BattleCruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/594/"",
name: ""Deadspace Angel Cartel Battleship""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/595/"",
name: ""Deadspace Angel Cartel Cruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/596/"",
name: ""Deadspace Angel Cartel Destroyer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/597/"",
name: ""Deadspace Angel Cartel Frigate""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/602/"",
name: ""Deadspace Blood Raiders BattleCruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/603/"",
name: ""Deadspace Blood Raiders Battleship""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/604/"",
name: ""Deadspace Blood Raiders Cruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/605/"",
name: ""Deadspace Blood Raiders Destroyer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/606/"",
name: ""Deadspace Blood Raiders Frigate""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/611/"",
name: ""Deadspace Guristas BattleCruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/612/"",
name: ""Deadspace Guristas Battleship""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/613/"",
name: ""Deadspace Guristas Cruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/614/"",
name: ""Deadspace Guristas Destroyer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/615/"",
name: ""Deadspace Guristas Frigate""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/620/"",
name: ""Deadspace Sansha's Nation BattleCruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/621/"",
name: ""Deadspace Sansha's Nation Battleship""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/622/"",
name: ""Deadspace Sansha's Nation Cruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/623/"",
name: ""Deadspace Sansha's Nation Destroyer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/624/"",
name: ""Deadspace Sansha's Nation Frigate""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/629/"",
name: ""Deadspace Serpentis BattleCruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/630/"",
name: ""Deadspace Serpentis Battleship""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/631/"",
name: ""Deadspace Serpentis Cruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/632/"",
name: ""Deadspace Serpentis Destroyer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/633/"",
name: ""Deadspace Serpentis Frigate""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/638/"",
name: ""Navigation Computer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/639/"",
name: ""Electronic Warfare Drone""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/640/"",
name: ""Logistic Drone""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/641/"",
name: ""Stasis Webifying Drone""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/642/"",
name: ""Super Gang Enhancer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/643/"",
name: ""Carrier Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/644/"",
name: ""Drone Navigation Computer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/645/"",
name: ""Drone Damage Modules""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/646/"",
name: ""Drone Tracking Modules""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/647/"",
name: ""Drone Control Range Module""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/648/"",
name: ""Advanced Rocket""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/649/"",
name: ""Freight Container""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/350858/"",
name: ""Infantry Weapons""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/651/"",
name: ""Super Weapon Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/652/"",
name: ""Lease""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/653/"",
name: ""Advanced Light Missile""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/654/"",
name: ""Advanced Heavy Assault Missile""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/655/"",
name: ""Advanced Heavy Missile""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/656/"",
name: ""Advanced Cruise Missile""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/657/"",
name: ""Advanced Torpedo""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/658/"",
name: ""Cynosural Field""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/451/"",
name: ""Bistot""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/660/"",
name: ""Energy Vampire Slayer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/661/"",
name: ""Simple Biochemical Reactions""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/662/"",
name: ""Complex Biochemical Reactions""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/663/"",
name: ""Mercoxit Mining Crystal""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/665/"",
name: ""Mission Amarr Empire Frigate""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/666/"",
name: ""Mission Amarr Empire Battlecruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/667/"",
name: ""Mission Amarr Empire Battleship""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/668/"",
name: ""Mission Amarr Empire Cruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/669/"",
name: ""Mission Amarr Empire Destroyer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/670/"",
name: ""Mission Amarr Empire Other""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/671/"",
name: ""Mission Caldari State Frigate""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/672/"",
name: ""Mission Caldari State Battlecruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/673/"",
name: ""Mission Caldari State Cruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/674/"",
name: ""Mission Caldari State Battleship""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/675/"",
name: ""Mission Caldari State Other""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/676/"",
name: ""Mission Caldari State Destroyer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/677/"",
name: ""Mission Gallente Federation Frigate""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/678/"",
name: ""Mission Gallente Federation Cruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/679/"",
name: ""Mission Gallente Federation Destroyer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/680/"",
name: ""Mission Gallente Federation Battleship""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/681/"",
name: ""Mission Gallente Federation Battlecruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/682/"",
name: ""Mission Gallente Federation Other""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/683/"",
name: ""Mission Minmatar Republic Frigate""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/684/"",
name: ""Mission Minmatar Republic Destroyer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/685/"",
name: ""Mission Minmatar Republic Battlecruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/686/"",
name: ""Mission Minmatar Republic Other""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/687/"",
name: ""Mission Khanid Frigate""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/688/"",
name: ""Mission Khanid Destroyer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/689/"",
name: ""Mission Khanid Cruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/690/"",
name: ""Mission Khanid Battlecruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/691/"",
name: ""Mission Khanid Battleship""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/692/"",
name: ""Mission Khanid Other""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/693/"",
name: ""Mission CONCORD Frigate""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/694/"",
name: ""Mission CONCORD Destroyer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/695/"",
name: ""Mission CONCORD Cruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/696/"",
name: ""Mission CONCORD Battlecruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/697/"",
name: ""Mission CONCORD Battleship""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/698/"",
name: ""Mission CONCORD Other""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/699/"",
name: ""Mission Mordu Frigate""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/700/"",
name: ""Mission Mordu Destroyer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/701/"",
name: ""Mission Mordu Cruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/702/"",
name: ""Mission Mordu Battlecruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/703/"",
name: ""Mission Mordu Battleship""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/704/"",
name: ""Mission Mordu Other""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/705/"",
name: ""Mission Minmatar Republic Cruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/706/"",
name: ""Mission Minmatar Republic Battleship""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/707/"",
name: ""Jump Portal Array""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/709/"",
name: ""Scanner Array""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/710/"",
name: ""Logistics Array""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/711/"",
name: ""Harvestable Cloud""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/712/"",
name: ""Biochemical Material""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/715/"",
name: ""Destructible Agents In Space""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/716/"",
name: ""Data Interfaces""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/718/"",
name: ""Booster Blueprints""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/721/"",
name: ""Temp""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/722/"",
name: ""Advanced Hybrid Charge Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/723/"",
name: ""Tractor Beam Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/724/"",
name: ""Implant Blueprints""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/725/"",
name: ""Advanced Projectile Ammo Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/726/"",
name: ""Advanced Frequency Crystal Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/727/"",
name: ""Mining Crystal Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/728/"",
name: ""Decryptors - Amarr""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/729/"",
name: ""Decryptors - Minmatar""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/730/"",
name: ""Decryptors - Gallente""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/731/"",
name: ""Decryptors - Caldari""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/732/"",
name: ""Decryptors - Sleepers""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/733/"",
name: ""Decryptors - Yan Jung""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/734/"",
name: ""Decryptors - Takmahl""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/735/"",
name: ""Decryptors - Talocan""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/737/"",
name: ""Gas Cloud Harvester""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/738/"",
name: ""Cyber Armor""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/739/"",
name: ""Cyber Drones""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/740/"",
name: ""Cyber Electronic Systems""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/741/"",
name: ""Cyber Engineering""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/742/"",
name: ""Cyber Gunnery""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/743/"",
name: ""Cyber Production""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/744/"",
name: ""Cyber Leadership""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/745/"",
name: ""Cyber Learning""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/746/"",
name: ""Cyber Missile""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/747/"",
name: ""Cyber Navigation""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/748/"",
name: ""Cyber Science""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/749/"",
name: ""Cyber Shields""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/750/"",
name: ""Cyber Social""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/751/"",
name: ""Cyber Trade""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/753/"",
name: ""ECM Enhancer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/754/"",
name: ""Salvaged Materials""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/755/"",
name: ""Asteroid Rogue Drone BattleCruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/756/"",
name: ""Asteroid Rogue Drone Battleship""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/757/"",
name: ""Asteroid Rogue Drone Cruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/758/"",
name: ""Asteroid Rogue Drone Destroyer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/759/"",
name: ""Asteroid Rogue Drone Frigate""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/760/"",
name: ""Asteroid Rogue Drone Hauler""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/761/"",
name: ""Asteroid Rogue Drone Swarm""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/762/"",
name: ""Inertial Stabilizer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/763/"",
name: ""Nanofiber Internal Structure""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/764/"",
name: ""Overdrive Injector System""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/765/"",
name: ""Expanded Cargohold""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/766/"",
name: ""Power Diagnostic System""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/767/"",
name: ""Capacitor Power Relay""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/768/"",
name: ""Capacitor Flux Coil""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/769/"",
name: ""Reactor Control Unit""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/770/"",
name: ""Shield Flux Coil""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/771/"",
name: ""Missile Launcher Heavy Assault""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/772/"",
name: ""Heavy Assault Missile""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/773/"",
name: ""Rig Armor""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/774/"",
name: ""Rig Shield""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/775/"",
name: ""Rig Energy Weapon""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/776/"",
name: ""Rig Hybrid Weapon""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/777/"",
name: ""Rig Projectile Weapon""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/778/"",
name: ""Rig Drones""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/779/"",
name: ""Rig Launcher""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/781/"",
name: ""Rig Core""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/782/"",
name: ""Rig Navigation""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/783/"",
name: ""Cyber X Specials""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/784/"",
name: ""Large Collidable Ship""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/786/"",
name: ""Rig Electronic Systems""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/787/"",
name: ""Rig Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/789/"",
name: ""Asteroid Angel Cartel Commander Frigate""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/790/"",
name: ""Asteroid Angel Cartel Commander Cruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/791/"",
name: ""Asteroid Blood Raiders Commander Cruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/792/"",
name: ""Asteroid Blood Raiders Commander Frigate""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/793/"",
name: ""Asteroid Angel Cartel Commander BattleCruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/794/"",
name: ""Asteroid Angel Cartel Commander Destroyer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/795/"",
name: ""Asteroid Blood Raiders Commander BattleCruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/796/"",
name: ""Asteroid Blood Raiders Commander Destroyer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/797/"",
name: ""Asteroid Guristas Commander BattleCruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/798/"",
name: ""Asteroid Guristas Commander Cruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/799/"",
name: ""Asteroid Guristas Commander Destroyer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/800/"",
name: ""Asteroid Guristas Commander Frigate""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/801/"",
name: ""Deadspace Rogue Drone BattleCruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/802/"",
name: ""Deadspace Rogue Drone Battleship""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/803/"",
name: ""Deadspace Rogue Drone Cruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/804/"",
name: ""Deadspace Rogue Drone Destroyer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/805/"",
name: ""Deadspace Rogue Drone Frigate""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/806/"",
name: ""Deadspace Rogue Drone Swarm""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/807/"",
name: ""Asteroid Sansha's Nation Commander BattleCruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/808/"",
name: ""Asteroid Sansha's Nation Commander Cruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/809/"",
name: ""Asteroid Sansha's Nation Commander Destroyer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/810/"",
name: ""Asteroid Sansha's Nation Commander Frigate""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/811/"",
name: ""Asteroid Serpentis Commander BattleCruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/812/"",
name: ""Asteroid Serpentis Commander Cruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/813/"",
name: ""Asteroid Serpentis Commander Destroyer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/814/"",
name: ""Asteroid Serpentis Commander Frigate""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/815/"",
name: ""Clone Vat Bay""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/816/"",
name: ""Mission Generic Battleships""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/817/"",
name: ""Mission Generic Cruisers""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/818/"",
name: ""Mission Generic Frigates""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/819/"",
name: ""Deadspace Overseer Frigate""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/820/"",
name: ""Deadspace Overseer Cruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/821/"",
name: ""Deadspace Overseer Battleship""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/822/"",
name: ""Mission Thukker Battlecruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/823/"",
name: ""Mission Thukker Battleship""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/824/"",
name: ""Mission Thukker Cruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/825/"",
name: ""Mission Thukker Destroyer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/826/"",
name: ""Mission Thukker Frigate""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/827/"",
name: ""Mission Thukker Other""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/828/"",
name: ""Mission Generic Battle Cruisers""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/829/"",
name: ""Mission Generic Destroyers""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/830/"",
name: ""Covert Ops""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/831/"",
name: ""Interceptor""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/832/"",
name: ""Logistics""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/833/"",
name: ""Force Recon Ship""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/834/"",
name: ""Stealth Bomber""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/835/"",
name: ""Station Upgrade Platform""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/836/"",
name: ""Station Improvement Platform""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/837/"",
name: ""Energy Neutralizing Battery""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/838/"",
name: ""Cynosural Generator Array""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/839/"",
name: ""Cynosural System Jammer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/840/"",
name: ""Structure Repair Array""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/841/"",
name: ""Starbase - Control Tower Blueprints""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/842/"",
name: ""Remote ECM Burst""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/843/"",
name: ""Asteroid Rogue Drone Commander BattleCruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/844/"",
name: ""Asteroid Rogue Drone Commander Battleship""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/845/"",
name: ""Asteroid Rogue Drone Commander Cruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/846/"",
name: ""Asteroid Rogue Drone Commander Destroyer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/847/"",
name: ""Asteroid Rogue Drone Commander Frigate""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/848/"",
name: ""Asteroid Angel Cartel Commander Battleship""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/849/"",
name: ""Asteroid Blood Raiders Commander Battleship""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/850/"",
name: ""Asteroid Guristas Commander Battleship""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/851/"",
name: ""Asteroid Sansha's Nation Commander Battleship""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/852/"",
name: ""Asteroid Serpentis Commander Battleship""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/853/"",
name: ""Starbase - Laser Battery Blueprints""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/854/"",
name: ""Starbase - Projectile Battery Blueprints""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/855/"",
name: ""Starbase - Hybrid Battery Blueprints""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/351064/"",
name: ""Infantry Dropsuits""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/857/"",
name: ""Starbase - Warp Scrambling Battery Blueprints""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/858/"",
name: ""Starbase - Stasis Webification Battery Blueprints""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/859/"",
name: ""Starbase - Sensor Dampening Array Blueprints""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/860/"",
name: ""Starbase - Energy Neutralizing Battery Blueprints""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/861/"",
name: ""Mission Fighter Drone""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/862/"",
name: ""Missile Launcher Bomb""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/863/"",
name: ""Bomb ECM""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/864/"",
name: ""Bomb Energy""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/865/"",
name: ""Mission Amarr Empire Carrier""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/866/"",
name: ""Mission Caldari State Carrier""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/867/"",
name: ""Mission Gallente Federation Carrier""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/868/"",
name: ""Mission Minmatar Republic Carrier""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/870/"",
name: ""Remote Hull Repairer Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/871/"",
name: ""Starbase - Missile Battery Blueprints""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/872/"",
name: ""Outpost Improvements""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/873/"",
name: ""Capital Construction Components""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/874/"",
name: ""Disruptable Station Services""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/875/"",
name: ""Mission Faction Transports""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/876/"",
name: ""Outpost Upgrades""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/877/"",
name: ""Target Painting Battery""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/878/"",
name: ""Cloak Enhancements""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/879/"",
name: ""Slave Reception""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/880/"",
name: ""Sleeper Components""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/881/"",
name: ""Freedom Programs""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/882/"",
name: ""Enslavement Programs""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/883/"",
name: ""Capital Industrial Ship""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/884/"",
name: ""Test Compressed Ore""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/885/"",
name: ""Cosmic Anomaly""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/886/"",
name: ""Rogue Drone Components""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/888/"",
name: ""Ore Compression Blueprints""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/889/"",
name: ""Ore Enhancement Blueprints""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/890/"",
name: ""Ice Compression Blueprints""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/891/"",
name: ""Starbase - Mobile Laboratory Blueprints""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/892/"",
name: ""Planet Satellites""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/893/"",
name: ""Electronic Attack Ship""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/894/"",
name: ""Heavy Interdiction Cruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/367487/"",
name: ""Services ""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/896/"",
name: ""Rig Security Transponder""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/897/"",
name: ""Covert Beacon""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/898/"",
name: ""Black Ops""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/899/"",
name: ""Warp Disrupt Field Generator""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/900/"",
name: ""Marauder""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/901/"",
name: ""Mining Enhancer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/902/"",
name: ""Jump Freighter""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/903/"",
name: ""Ancient Compressed Ice""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/904/"",
name: ""Rig Mining""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/905/"",
name: ""Covert Cynosural Field Generator""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/906/"",
name: ""Combat Recon Ship""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/907/"",
name: ""Tracking Script""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/908/"",
name: ""Warp Disruption Script""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/909/"",
name: ""Tracking Disruption Script""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/910/"",
name: ""Sensor Booster Script""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/911/"",
name: ""Sensor Dampener Script""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/912/"",
name: ""Script Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/351121/"",
name: ""Infantry Modules""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/914/"",
name: ""Advanced Capital Construction Component Blueprints""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/915/"",
name: ""Capital Construction Blueprints""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/916/"",
name: ""Nanite Repair Paste""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/917/"",
name: ""Data Miner Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/918/"",
name: ""Scan Probe Launcher Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/920/"",
name: ""Effect Beacon""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/922/"",
name: ""Capture Point""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/924/"",
name: ""Mission Faction Battleship""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/925/"",
name: ""FW Infrastructure Hub""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/927/"",
name: ""Mission Faction Industrials""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/934/"",
name: ""Zombie Entities""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/935/"",
name: ""WorldSpace""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/937/"",
name: ""Decorations""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/940/"",
name: ""Furniture""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/941/"",
name: ""Industrial Command Ship""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/943/"",
name: ""Game Time""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/944/"",
name: ""Capital Industrial Ship Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/945/"",
name: ""Industrial Command Ship Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1182/"",
name: ""FW Minmatar Republic Cruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/952/"",
name: ""Mission Container""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/954/"",
name: ""Defensive Systems""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/955/"",
name: ""Electronic Systems""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/956/"",
name: ""Offensive Systems""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/957/"",
name: ""Propulsion Systems""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/958/"",
name: ""Engineering Systems""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/959/"",
name: ""Deadspace Sleeper Sleepless Sentinel""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/960/"",
name: ""Deadspace Sleeper Awakened Sentinel""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1184/"",
name: ""FW Caldari State Battlecruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/963/"",
name: ""Strategic Cruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/964/"",
name: ""Hybrid Tech Components""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/965/"",
name: ""Hybrid Component Blueprints""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/966/"",
name: ""Ancient Salvage""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/967/"",
name: ""Wormhole Minerals""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/971/"",
name: ""Sleeper Propulsion Relics""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/972/"",
name: ""Obsolete Probes""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/973/"",
name: ""Subsystem Blueprints""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/974/"",
name: ""Hybrid Polymers""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/976/"",
name: ""Festival Charges Expired""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/977/"",
name: ""Hybrid Reactions""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/979/"",
name: ""Decryptors - Hybrid""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/982/"",
name: ""Deadspace Sleeper Sleepless Defender""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/983/"",
name: ""Deadspace Sleeper Sleepless Patroller""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/984/"",
name: ""Deadspace Sleeper Awakened Defender""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/985/"",
name: ""Deadspace Sleeper Awakened Patroller""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/986/"",
name: ""Deadspace Sleeper Emergent Defender""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/987/"",
name: ""Deadspace Sleeper Emergent Patroller""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/367580/"",
name: ""Agents""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/990/"",
name: ""Sleeper Electronics Relics""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/991/"",
name: ""Sleeper Offensive Relics""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/992/"",
name: ""Sleeper Engineering Relics""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/993/"",
name: ""Sleeper Defensive Relics""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/995/"",
name: ""Secondary Sun""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/996/"",
name: ""Strategic Cruiser Blueprints""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/997/"",
name: ""Sleeper Hull Relics""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/351210/"",
name: ""Infantry Vehicles""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1003/"",
name: ""Territorial Claim Unit""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1004/"",
name: ""Defense Bunkers""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1005/"",
name: ""Sovereignty Blockade Unit""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1006/"",
name: ""Mission Faction Cruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1007/"",
name: ""Mission Faction Frigate""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1010/"",
name: ""Compact Citadel Torpedo""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1012/"",
name: ""Infrastructure Hub""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1013/"",
name: ""Supercarrier Blueprints""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1016/"",
name: ""Strategic Upgrades""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1019/"",
name: ""Citadel Cruise""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1020/"",
name: ""Industrial Upgrades""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1021/"",
name: ""Military Upgrades""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1022/"",
name: ""Prototype Exploration Ship""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1023/"",
name: ""Fighter Bomber""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1025/"",
name: ""Orbital Infrastructure""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1026/"",
name: ""Extractors""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1027/"",
name: ""Command Centers""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1028/"",
name: ""Processors""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1029/"",
name: ""Storage Facilities""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1030/"",
name: ""Spaceports""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1031/"",
name: ""Planetary Resources""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1032/"",
name: ""Planet Solid""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1033/"",
name: ""Planet Liquid-Gas""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1034/"",
name: ""Refined Commodities""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1035/"",
name: ""Planet Organic""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1036/"",
name: ""Planetary Links""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1040/"",
name: ""Specialized Commodities""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1041/"",
name: ""Advanced Commodities""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1042/"",
name: ""Basic Commodities""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1045/"",
name: ""Sovereignty Structure Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1046/"",
name: ""Nanite Repair Paste Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1048/"",
name: ""Starbase Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1051/"",
name: ""Incursion Sansha's Nation Industrial""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1052/"",
name: ""Incursion Sansha's Nation Capital""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1053/"",
name: ""Incursion Sansha's Nation Frigate""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1054/"",
name: ""Incursion Sansha's Nation Cruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1056/"",
name: ""Incursion Sansha's Nation Battleship""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1063/"",
name: ""Extractor Control Units""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1067/"",
name: ""MaterialZone""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1068/"",
name: ""DetailMesh""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1071/"",
name: ""Flashpoint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1073/"",
name: ""Test Orbitals""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1079/"",
name: ""Generic""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1081/"",
name: ""Mercenary Bases""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1082/"",
name: ""Capsuleer Bases""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1083/"",
name: ""Eyewear""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1084/"",
name: ""Tattoos""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1085/"",
name: ""Piercings""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1086/"",
name: ""Scars""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1087/"",
name: ""Mid Layer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1088/"",
name: ""Outer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1089/"",
name: ""Tops""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1090/"",
name: ""Bottoms""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1091/"",
name: ""Footwear""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1092/"",
name: ""Hair Styles""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1093/"",
name: ""Makeup""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1105/"",
name: ""Lens Flares""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1106/"",
name: ""Orbital Construction Platform""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1107/"",
name: ""Particle Systems""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1108/"",
name: ""Animated Lights""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1109/"",
name: ""Audio""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1110/"",
name: ""Point Lights""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1111/"",
name: ""Box Lights""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1112/"",
name: ""Spot Lights""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1118/"",
name: ""Surface Infrastructure Prefab Units""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1121/"",
name: ""Perception Points""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1122/"",
name: ""Salvager""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1123/"",
name: ""Salvager Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1126/"",
name: ""PhysicalPortals""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1136/"",
name: ""Fuel Block""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1137/"",
name: ""Fuel Block Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1139/"",
name: ""Mining Laser Upgrade Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1141/"",
name: ""Research Data""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1142/"",
name: ""Energy Neutralizer Drone Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1143/"",
name: ""Electronic Warfare Drone Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1144/"",
name: ""Logistic Drone Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1145/"",
name: ""Fighter Bomber Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1146/"",
name: ""Fighter Drone Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1147/"",
name: ""Stasis Webifying Drone Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1149/"",
name: ""Mobile Jump Disruptor""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1150/"",
name: ""Armor Resistance Shift Hardener""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1151/"",
name: ""Armor Resistance Shift Hardener Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1152/"",
name: ""Drone Damage Module Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1153/"",
name: ""Shield Booster Script""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1154/"",
name: ""Target Breaker""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1155/"",
name: ""Target Breaker Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1156/"",
name: ""Fueled Shield Booster""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1157/"",
name: ""Fueled Shield Booster Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1158/"",
name: ""Heavy Defender Missile""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1159/"",
name: ""Salvage Drone""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1160/"",
name: ""Survey Probe Blueprints""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1162/"",
name: ""Container Blueprints""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1165/"",
name: ""Satellite""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1166/"",
name: ""FW Minmatar Republic Frigate""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1167/"",
name: ""FW Caldari State Frigate""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1168/"",
name: ""FW Gallente Federation Frigate""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1169/"",
name: ""FW Amarr Empire Frigate""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1174/"",
name: ""Asteroid Rogue Drone Officer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1175/"",
name: ""FW Amarr Empire Destroyer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1176/"",
name: ""FW Caldari State Destroyer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1177/"",
name: ""FW Gallente Federation Destroyer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1178/"",
name: ""FW Minmatar Republic Destroyer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1179/"",
name: ""FW Amarr Empire Cruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1180/"",
name: ""FW Caldari State Cruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1181/"",
name: ""FW Gallente Federation Cruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/367774/"",
name: ""Salvage Containers""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1183/"",
name: ""FW Amarr Empire Battlecruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/367776/"",
name: ""Salvage Decryptors""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1185/"",
name: ""FW Gallente Federation Battlecruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1186/"",
name: ""FW Minmatar Republic Battlecruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1189/"",
name: ""Micro Jump Drive""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1190/"",
name: ""Salvage Drone Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1191/"",
name: ""Micro Jump Drive Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1194/"",
name: ""Special Edition Commodities""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1195/"",
name: ""Tournament Cards: New Eden Open YC 114""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1197/"",
name: ""Special Edition Commodity Blueprints""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1198/"",
name: ""Orbital Target""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1199/"",
name: ""Fueled Armor Repairer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1200/"",
name: ""Fueled Armor Repairer Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1201/"",
name: ""Attack Battlecruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1202/"",
name: ""Blockade Runner""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1206/"",
name: ""Security Tags""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1207/"",
name: ""Scatter Container""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1209/"",
name: ""Shields""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1210/"",
name: ""Armor""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1212/"",
name: ""Personal Hangar""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1213/"",
name: ""Targeting""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1216/"",
name: ""Engineering""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1217/"",
name: ""Scanning""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1218/"",
name: ""Resource Processing""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1220/"",
name: ""Neural Enhancement""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1222/"",
name: ""ECM Stabilizer Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1223/"",
name: ""Scanning Upgrade""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1224/"",
name: ""Scanning Upgrade Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1225/"",
name: ""Tournament Cards: Alliance Tournament All Stars""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1226/"",
name: ""Survey Probe Launcher""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1227/"",
name: ""Survey Probe Launcher Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1228/"",
name: ""Cyber Targeting""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1229/"",
name: ""Cyber Resource Processing""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1230/"",
name: ""Cyber Scanning""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1231/"",
name: ""Cyber Biology""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1232/"",
name: ""Rig Resource Processing""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1233/"",
name: ""Rig Scanning""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1234/"",
name: ""Rig Targeting""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1238/"",
name: ""Scanning Upgrade Time""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1239/"",
name: ""Scanning Upgrade Time Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1240/"",
name: ""Subsystems""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1241/"",
name: ""Planet Management""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1245/"",
name: ""Missile Launcher Rapid Heavy""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1246/"",
name: ""Mobile Depot""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1247/"",
name: ""Mobile Siphon Unit""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1248/"",
name: ""Empire Bounty Reimbursement Tags""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1249/"",
name: ""Mobile Cyno Inhibitor""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1250/"",
name: ""Mobile Tractor Unit""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1252/"",
name: ""Ghost Sites Angel Cartel Cruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1255/"",
name: ""Ghost Sites Blood Raiders Cruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1259/"",
name: ""Ghost Sites Guristas Cruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1262/"",
name: ""Ghost Sites Serpentis Cruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1265/"",
name: ""Ghost Sites Sanshas Cruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1267/"",
name: ""Mobile Siphon Unit Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1268/"",
name: ""Mobile Cynosural Inhibitor Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1269/"",
name: ""Mobile Depot Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1270/"",
name: ""Mobile Tractor Unit Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1271/"",
name: ""Prosthetics""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1273/"",
name: ""Encounter Surveillance System""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1274/"",
name: ""Mobile Decoy Unit""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1275/"",
name: ""Mobile Scan Inhibitor""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1276/"",
name: ""Mobile Micro Jump Unit""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1277/"",
name: ""Encounter Surveillance System Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1282/"",
name: ""Compression Array""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1283/"",
name: ""Expedition Frigate""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1285/"",
name: ""Asteroid Mordus Legion Commander Frigate""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1286/"",
name: ""Asteroid Mordus Legion Commander Cruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1287/"",
name: ""Asteroid Mordus Legion Commander Battleship""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1288/"",
name: ""Ghost Sites Mordu's Legion""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1289/"",
name: ""Warp Accelerator""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1292/"",
name: ""Drone Tracking Enhancer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1293/"",
name: ""Mobile Scan Inhibitor Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1294/"",
name: ""Mobile Micro Jump Unit Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1295/"",
name: ""Mobile Decoy Unit Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1297/"",
name: ""Mobile Vault""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1299/"",
name: ""Jump Drive Economizer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1301/"",
name: ""Services""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1304/"",
name: ""Generic Decryptor""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1305/"",
name: ""Tactical Destroyer""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1306/"",
name: ""Ship Modifiers""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1307/"",
name: ""Roaming Sleepers Cruiser""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1308/"",
name: ""Rig Anchor""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1309/"",
name: ""Tactical Destroyer Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1310/"",
name: ""Drifter Battleship""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1311/"",
name: ""Super Kerr-Induced Nanocoatings""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1312/"",
name: ""Observatory Structures""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1313/"",
name: ""Entosis Link""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1314/"",
name: ""Unknown Components""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1316/"",
name: ""Entosis Command Node""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1317/"",
name: ""Infrastructure Upgrade Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1318/"",
name: ""Entosis Link Blueprint""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1319/"",
name: ""Miscellaneous""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1320/"",
name: ""Citadel""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1321/"",
name: ""Structure Citadel Service Module""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1322/"",
name: ""Structure Drilling Service Module""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1323/"",
name: ""Structure Observatory Service Module""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1324/"",
name: ""Structure Stargate Service Module""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1325/"",
name: ""Structure Administration Service Module""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1326/"",
name: ""Structure Advertisement Service Module""
},
{
href: ""https://public-crest.eveonline.com/inventory/groups/1327/"",
name: ""Structure Anti-Ship Launcher""
}
],
next: {
href: ""https://public-crest.eveonline.com/inventory/groups/?page=2""
},
totalCount: 1215,
pageCount_str: ""2""
}";
    }
}
