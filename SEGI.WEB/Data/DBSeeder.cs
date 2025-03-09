using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SEGI.Core.Constants;
using SEGI.WEB.Data;
using SEGI.WEB.Core.Enums;

namespace SEGI.Data
{

    public static class DBSeeder
    {

        public static IHost SeedDb(this IHost webHost)
        {
            using (var scope = webHost.Services.CreateScope())
            {
                try
                {
                    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    SeedWhoWeAres(context);
                    SeedCoreValue(context);
                    SeedSector(context);
                    SeedSectorItems(context);
                    SeedAnaltic(context);
                    SeedBannerAboutUs(context);
                    SeedLegacyAboutUs(context);
                    SeedOurVisionAboutUs(context);
                    SeedOurMissionAboutUs(context);
                    SeedOurCoreValue(context);
                    SeedOurCoreValueItems(context);
                    SeedOurHistoryAboutUs(context);
                    SeedTeam(context);
                    SeedArticle(context);
                    SeedGeneralEngineeringServicesItem(context);
                    SeedPMCServicesItems(context);
                    SeedProcessSafetyStudies(context);
                    SeedProcessSafetyStudiesItems(context);
                    SeedBannerMain(context);
                    SeedCategories(context);
                    SeedNews(context);
                    SeedHistory(context);
                    SeedFixedItem(context);

                    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                    roleManager.SeedRoles().Wait();
                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                    userManager.SeedUsers().Wait();
                    context.Database.Migrate();

                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex.Message);
                    //throw;
                }
            }
            return webHost;
        }



        public static async Task SeedRoles(this RoleManager<IdentityRole> roleManager)
        {
            if (await roleManager.Roles.AnyAsync()) return;

            await roleManager.CreateAsync(new IdentityRole(Constant.RolesFilter.SuperAdmin));
            await roleManager.CreateAsync(new IdentityRole(Constant.RolesFilter.User));

        }
        public static void SeedWhoWeAres(this ApplicationDbContext context)
        {
            if (context.WhoWeAres.Any())
            {
                return;
            }
            context.WhoWeAres.AddRange(new WhoWeAre
            {
                Title = "Saudi Engineering Group International (SEGI)",
                Description = "Saudi Engineering Group International (SEGI) stands as a premier leader in engineering design and consultancy services, headquartered in Dammam, Eastern Province, Saudi Arabia. Founded in 1979 by Dr. Abdullah Ali A. Al-Saihati, SEGI has become a cornerstone of innovation and excellence in engineering, project management, and consultancy across critical sectors With over 1,000 multidisciplinary professionals—including expert consultants, engineers, designers, and project specialists—SEGI delivers world-class solutions for power & energy, hydrocarbon, water, and infrastructure projects throughout the Kingdom of Saudi Arabia.",
                Image = "About-US-Cover.webp"
            });
            context.SaveChanges();

        }
        public static void SeedCoreValue(this ApplicationDbContext context)
        {
            if (context.CoreValues.Any())
            {
                return;
            }
            context.CoreValues.AddRange(new CoreValue
            {
                Title = "Core Values",
                Description = "At the heart of everything we do are Integrity and Ethics, guiding us to always do what’s right. With Accountability, we take full responsibility for our actions, ensuring we remain Transparent in every decision.",
                Image = "news.webp",
                Link_Video = "https://player.vimeo.com/video/676810838?title=0&amp;byline=0&amp;portrait=0"
            });
            context.SaveChanges();

        }
        public static void SeedSector(this ApplicationDbContext context)
        {
            if (context.Sectors.Any())
            {
                return;
            }
            context.Sectors.AddRange(new Sector
            {
                Title = "Sectors",
                Description = "At SEGI, we bring decades of experience and innovative engineering solutions to the most critical industries shaping the Kingdom of Saudi Arabia and beyond. Our expertise spans across diverse sectors, enabling us to contribute to the development and growth of vital industries.",
                Image = "news.webp",
            });
            context.SaveChanges();

        }
        public static void SeedSectorItems(this ApplicationDbContext context)
        {
            if (context.SectorItems.Any())
            {
                return;
            }
            var sector1 = new SectorItem
            {
                Title = "Power and Energy",
                Description = "All Types of Power Plants\nSubstations from 33- 380 Kv.\nTransmission Lines and Underground",
                Image = "news.webp",
                Icon = "POWER.svg"
            };
            var sector2 = new SectorItem
            {
                Title = "Infrastructure",
                Description = "All Types of Power Plants\nSubstations from 33- 380 Kv.\nTransmission Lines and Underground",
                Image = "news.webp",
                Icon = "POWER.svg",
            };
            var sector3 = new SectorItem
            {
                Title = "Oil and Gas",
                Description = "All Types of Power Plants\nSubstations from 33- 380 Kv.\nTransmission Lines and Underground",
                Image = "news.webp",
                Icon = "OIL.svg"

            };
            var sector4 = new SectorItem
            {
                Title = "Mining",
                Description = "All Types of Power Plants\nSubstations from 33- 380 Kv.\nTransmission Lines and Underground",
                Image = "news.webp",
                Icon = "MINING.svg"
            };
            var sector5 = new SectorItem
            {
                Title = "Water Utilities",
                Description = "All Types of Power Plants\nSubstations from 33- 380 Kv.\nTransmission Lines and Underground",
                Image = "news.webp",
                Icon = "water-supply (1).svg"

            };
            var sector6 = new SectorItem
            {
                Title = "Health and Education",
                Description = "All Types of Power Plants\nSubstations from 33- 380 Kv.\nTransmission Lines and Underground",
                Image = "news.webp",
                Icon = "MEDICAL.svg"

            };
            var sector7 = new SectorItem
            {
                Title = "Petrochemicals",
                Description = "All Types of Power Plants\nSubstations from 33- 380 Kv.\nTransmission Lines and Underground",
                Image = "news.webp",
                Icon = "download.svg"

            };
            var sector8 = new SectorItem
            {
                Title = "Specialized Studies",
                Description = "All Types of Power Plants\nSubstations from 33- 380 Kv.\nTransmission Lines and Underground",
                Image = "news.webp",
                Icon = "download.svg"

            };
            context.SectorItems.AddRange(sector1, sector2, sector3, sector4, sector5, sector6, sector7, sector8);
            context.SaveChanges();

        }
        public static void SeedAnaltic(this ApplicationDbContext context)
        {
            if (context.Analitics.Any())
            {
                return;
            }
            var item1 = new Analitic
            {
                Title = "Employeea",
                Number = 12000
            };
            var item2 = new Analitic
            {
                Title = "Submitted Projects",
                Number = 80

            };
            var item3 = new Analitic
            {
                Title = "Number of Running Projects",
                Number = 45

            };
            var item4 = new Analitic
            {
                Title = "Number of Running Partners",
                Number = 66
            };

            context.Analitics.AddRange(item1, item2, item3, item4);
            context.SaveChanges();

        }
        public static void SeedBannerAboutUs(this ApplicationDbContext context)
        {
            if (context.BannerAboutUss.Any())
            {
                return;
            }
            context.BannerAboutUss.AddRange(new BannerAboutUs
            {
                Title = "Unrivaled Expertise Across Key Sectors",
                Description = "Unrivaled Expertise Across Key Sectors At SEGI, our specialized teams collaborate seamlessly across sectors to deliver integrated and exceptional results. Leveraging decades of cumulative experience, deep market knowledge, and advanced technical expertise, we empower projects with: Innovative engineering designs Comprehensive project management solutions Client-centric approaches ensuring value and efficiency Our ability to integrate stakeholders, anticipate project needs, and adapt to local challenges sets us apart as a trusted partner in Saudi Arabia's industrial and infrastructure development.",
                Image = "AboutUsBanner.webp"
            });
            context.SaveChanges();

        }
        public static void SeedLegacyAboutUs(this ApplicationDbContext context)
        {
            if (context.LegacyAboutUss.Any())
            {
                return;
            }
            context.LegacyAboutUss.AddRange(new LegacyAboutUs
            {
                Title = "A Legacy of Excellence Since 1979",
                Description = "For over 45 years, " +
                "SEGI has established an extensive network of capabilities to manage and deliver multidisciplinary projects with precision." +
                " Our branches in Riyadh and Jeddah ensure close proximity to clients, enabling us to provide timely coordination," +
                " responsive service, and unmatched quality." +
                " Whether supporting the development of vital energy systems, pioneering hydrocarbon projects, " +
                "or designing sustainable water and infrastructure solutions, " +
                "SEGI remains committed to driving progress and exceeding client expectations at every stage. " +
                "SEGI: Transforming Vision into Reality.",
                Image = "Legacy.webp"
            });
            context.SaveChanges();

        }
        public static void SeedOurVisionAboutUs(this ApplicationDbContext context)
        {
            if (context.OurVisionAboutUss.Any())
            {
                return;
            }
            context.OurVisionAboutUss.AddRange(new OurVisionAboutUs
            {
                Title = "Our Vision",
                Description = "To become the partner of choice for innovative engineering and consultancy solutions," +
                " driving sustainable value and transformative progress for our clients and communities.",
                Image = "ourvision.webp"
            });
            context.SaveChanges();

        }
        public static void SeedOurMissionAboutUs(this ApplicationDbContext context)
        {
            if (context.OurMissionAboutUss.Any())
            {
                return;
            }
            context.OurMissionAboutUss.AddRange(new OurMissionAboutUs
            {
                Title = "Our Mission",
                Description = "To deliver professional engineering services that elevate quality standards in every project, prioritize safety through precision and expertise," +
                " and champion environmental preservation with sustainable and forward-thinking solutions.\r\nWe strive to exceed expectations," +
                "foster innovation, and contribute to a better, safer, and greener future.",
                Image = "ourMission.webp"
            });
            context.SaveChanges();

        }
        public static void SeedOurCoreValue(this ApplicationDbContext context)
        {
            if (context.OurCoreValues.Any())
            {
                return;
            }
            context.OurCoreValues.AddRange(new OurCoreValue
            {
                Title = "Our Core Values",
                Image = "news.webp",
            });
            context.SaveChanges();

        }
        public static void SeedOurCoreValueItems(this ApplicationDbContext context)
        {
            if (context.OurCoreValueItems.Any())
            {
                return;
            }
            var item1 = new OurCoreValueItem
            {
                Title = "Integrity",
                Description = "Upholding the highest ethical standards and honesty in everything we do.\r\n\r\n"
            };
            var item2 = new OurCoreValueItem
            {
                Title = "Accountability\r\n",
                Description = "Taking ownership of our actions and delivering on our commitments\r\n\r\n",

            };
            var item3 = new OurCoreValueItem
            {
                Title = "Trustworthiness",
                Description = "Maintaining open, clear, and honest communication with all stakeholders.\r\n\r\n",


            };
            var item4 = new OurCoreValueItem
            {
                Title = "Transparency",
                Description = "Proactively recognizing hazards for risk mitigation.\r\n\r\n",

            };
            var item5 = new OurCoreValueItem
            {
                Title = "Empowerment",
                Description = "Maintaining open, clear, and honest communication with all stakeholders.\r\n\r\nMaintaining open, clear, and honest communication with all stakeholders.\r\n\r\n",


            };
            var item6 = new OurCoreValueItem
            {
                Title = "Ethics",
                Description = "Proactively recognizing hazards for risk mitigation."

            };

            context.OurCoreValueItems.AddRange(item1, item2, item3, item4, item5, item6);
            context.SaveChanges();

        }
        public static void SeedOurHistoryAboutUs(this ApplicationDbContext context)
        {
            if (context.OurHistoryAboutUss.Any())
            {
                return;
            }
            context.OurHistoryAboutUss.AddRange(new OurHistoryAboutUs
            {
                Title = "Our History",
                Description = "SEGI’s important milestone during its 40 years of providing Engineering and PMC services in the Kingdom of Saudi Arabia.",
                Image = "ourMission.webp"
            });
            context.SaveChanges();

        }
        public static void SeedTeam(this ApplicationDbContext context)
        {
            if (context.Teams.Any())
            {
                return;
            }
            var team1 = new Team
            {
                FullName = "Dr. ABDULLAH ALI A ALSAIHATI",
                Description = "Dr. ABDULLAH ALI A ALSAIHATI\nGroup Chairman",
                Image = "Team1.webp",
                Position = "Group Chairman",
                TeamType = TeamType.Board
            };
            var team2 = new Team
            {
                FullName = "Engr. NAJEEB ABDULLAH \r\nA ALSAIHATI",
                Description = "Engr. NAJEEB ABDULLAH \nGroup Chairman",
                Image = "Team2.webp",
                Position = "Vice Chairman",
                TeamType = TeamType.Board
            };
            var team3 = new Team
            {
                FullName = "Engr. ALI ABDULLAH A ALSAIHAT",
                Description = "Engr. ALI ABDULLAH A ALSAIHAT\nBoard Member",
                Image = "Team3.webp",
                Position = "Board Member",
                TeamType = TeamType.Board
            };
            var team4 = new Team
            {
                FullName = "Engr. ALI ABDULLAH\r\nA ALSAIHATI",
                Description = "Engr. ALI ABDULLAH\r\nA ALSAIHATI\nChief Executive Officer",
                Image = "Team4.webp",
                Position = "Chief Executive Officer",
                TeamType = TeamType.KeyExecutive
            };
            var team5 = new Team
            {
                FullName = "Engr. RAFEEK MOHAMED YUNUS",
                Description = "Engr. RAFEEK MOHAMED YUNUS  \nManaging Director",
                Image = "Team5.webp",
                Position = "Managing Director",
                TeamType = TeamType.KeyExecutive
            };
            var team6 = new Team
            {
                FullName = "Engr. MOHAMED GAD \r\nBASSOUNY ELHEFNAWY",
                Description = "Engr. MOHAMED GAD BASSOUNY ELHEFNAWY\nEngineering Director",
                Image = "Team1.webp",
                Position = "Engineering Director",
                TeamType = TeamType.KeyExecutive
            };
            context.Teams.AddRange(team1, team2, team3, team4, team5, team6);
            context.SaveChanges();

        }
        public static void SeedArticle(this ApplicationDbContext context)
        {
            if (context.Articles.Any())
            {
                return;
            }

            var team1 = new Article
            {
                Title = "General Engineering Services",
                Description = "At SEGI," +
                " we deliver a comprehensive suite of engineering and design consultancy services tailored to meet the evolving needs of our clients." +
                " Our solutions are designed to ensure precision, " +
                "efficiency, and excellence at every stage of a project lifecycle\r\n\r\n",
                Image = "ourMission.webp",
                SectionId = 1,
                PageType = PageType.Services
            };
            var team2 = new Article
            {
                Title = "Our Expertise\r\n",
                Description = "At SEGI," +
                " we deliver a comprehensive suite of engineering and design consultancy services tailored to meet the evolving needs of our clients." +
                " Our solutions are designed to ensure precision, efficiency, and excellence at every stage of a project lifecycle",
                Image = "ourMission.webp",
                SectionId = 3,
                PageType = PageType.Services
            };
            var team3 = new Article
            {
                Title = "Why Choose SEGI ?\r\n",
                Description = "With decades of proven experience," +
                " SEGI combines innovation, precision," +
                " and technical mastery to deliver superior engineering services that drive success." +
                " Partner with us to bring your projects to life with efficiency, quality, and confidence.\r\n\r\n",
                Image = "ourMission.webp",
                SectionId = 4,
                PageType = PageType.Services
            };
            var team4 = new Article
            {
                Title = "Project Management Consultancy (PMC) Services\r\n",
                Description = "At SEGI," +
                " we offer comprehensive Project Management Consultancy (PMC) services to ensure the successful execution of projects—from concept to completion." +
                " Our PMC team excels at providing end-to-end project management, construction oversight," +
                " and site supervision while adhering to the highest standards of quality, safety, and efficiency.",
                Image = "ourMission.webp",
                SectionId = 5,
                PageType = PageType.Services
            };
            var team5 = new Article
            {
                Title = "Our Expertise\r\n",
                Description = "At SEGI, our PMC team comprises highly qualified consultant engineers," +
                " project managers, and technical specialistswith extensive experience across diverse industries. " +
                "With a proven track record in delivering complex projects, we bring the following advantages to every assignment:" +
                " Industry Expertise: Deep knowledge of construction, energy, water, hydrocarbon, and infrastructure sectors. Global Partnerships: " +
                "Collaborations with leading international and specialized engineering firms to enhance project capabilities. Proven Performance:" +
                " Successful delivery of projects with an unwavering focus on quality, safety, and efficiency.",
                Image = "ourMission.webp",
                SectionId = 7,
                PageType = PageType.Services
            };
            var team6 = new Article
            {
                Title = "Ads",
                Description = "At SEGI, we deliver a comprehensive suite of engineering and design consultancy services tailored to meet the evolving needs of our clients." +
                " Our solutions are designed to ensure precision, efficiency, and excellence at every stage of a project lifecycle",
                Image = "ourMission.webp",
                SectionId = 8,
                PageType = PageType.Services
            };
            var team7 = new Article
            {
                Title = "Specialized Studies\r\n",
                Description = "At SEGI," +
                " we deliver highly specialized studies to support critical project decisions, ensuring systems operate safely," +
                " efficiently, and reliably. With a combination of advanced tools," +
                " in-depth expertise, and industry best practices," +
                " our services cover a wide range of specialized engineering studies, including:\r\n",
                Image = "ourMission.webp",
                SectionId = 9,
                PageType = PageType.Services
            };
            var team9 = new Article
            {
                Title = "Power System Studies\r\n",
                Description = "We provide comprehensive power system analyses to optimize performance," +
                "reliability, and safety in electrical networks." +
                " Power Flow Studies: Assessing power distribution and network efficiency." +
                " Short Circuit Studies: Identifying fault levels and ensuring equipment resilience." +
                " Load Calculation Studies: Accurate load forecasting for system optimization." +
                " Protection Setting & Coordination Studies: Ensuring system safety with proper protection schemes." +
                " Insulation & Coordination Studies: Optimizing insulation levels to prevent failures." +
                " Load Shedding: Managing power stability during critical events. Generation Rejection:" +
                " Evaluating system responses to generation disruptions. Reactive Power Assessment:" +
                " Maintaining voltage stability and optimizing power factor. Arc Flash Hazard Analysis: " +
                "Identifying hazards and ensuring personnel safety. Voltage Stability Analysis:" +
                " Preventing voltage collapse under varying load conditions. " +
                "Transient Stability Analysis: Evaluating system stability under dynamic conditions.",
                Image = "ourMission.webp",
                SectionId = 10,
                PageType = PageType.Services
            };
            var team10 = new Article
            {
                Title = "Advanced Power Tools\r\n",
                Description = "ETAP Studies: Leveraging ETAP software for comprehensive system analysis," +
                " modeling, and simulation. HVDC Feasibility Studies:" +
                " Assessing high-voltage direct current systems for long-distance power transmission.",
                Image = "ourMission.webp",
                SectionId = 11,
                PageType = PageType.Services
            };
            var team11 = new Article
            {
                Title = "Flow Assurance Studies\r\n",
                Description = "We ensure uninterrupted flow of fluids (oil, gas, or water) across pipelines and systems," +
                "addressing challenges such as blockages, thermal performance, and pressure drop..",
                Image = "ourMission.webp",
                SectionId = 12,
                PageType = PageType.Services
            };
            var team12 = new Article
            {
                Title = "Flow Assurance Studies\r\n",
                Description = "We ensure uninterrupted flow of fluids (oil, gas, or water) across pipelines and systems," +
    "addressing challenges such as blockages, thermal performance, and pressure drop..",
                Image = "ourMission.webp",
                SectionId = 13,
                PageType = PageType.Services
            };
            var team13 = new Article
            {
                Title = "Hydraulic Engineering Studies\r\n",
                Description = "Providing solutions for fluid mechanics challenges, including water distribution systems, stormwater management, and hydraulic structures.\r\n\r\n",
                Image = "ourMission.webp",
                SectionId = 14,
                PageType = PageType.Services
            };
            var team14 = new Article
            {
                Title = "Environmental Risk Assessments\r\n",
                Description = "We conduct thorough environmental studies to evaluate project impact, ensuring compliance with environmental regulations and sustainable practices.\r\n\r\n",
                Image = "ourMission.webp",
                SectionId = 15,
                PageType = PageType.Services
            };
            var team15 = new Article
            {
                Title = "Cathodic Protection Studies\r\n",
                Description = "Protecting metal structures such as pipelines, tanks, and offshore platforms from corrosion through advanced cathodic protection techniques.\r\n\r\n",
                Image = "ourMission.webp",
                SectionId = 16,
                PageType = PageType.Services
            };
            var team16 = new Article
            {
                Title = "Stress Analysis Studies\r\n",
                Description = "Ensuring the structural integrity of piping systems and components under mechanical, thermal, and pressure stresses.\r\n\r\n",
                Image = "ourMission.webp",
                SectionId = 17,
                PageType = PageType.Services
            };
            var team17 = new Article
            {
                Title = "Why SEGI ?\r\n",
                Description = "At SEGI, our specialized studies are conducted by a team of highly skilled experts equipped with advanced tools and software. Our solutions are: Accurate: Based on rigorous methodologies and industry standards. Comprehensive: Covering all critical areas to mitigate risks and optimize performance. Customized: Tailored to meet the unique needs of each project and client. By combining technical excellence with practical insights, SEGI ensures your systems operate at their highest potential, delivering safety, efficiency, and sustainability.\r\n\r\n",
                Image = "ourMission.webp",
                SectionId = 18,
                PageType = PageType.Services
            };

            var team2_1 = new Article
            {
                Title = "Sustainability at SEGI\r\n",
                Description = "At SEGI, sustainability is at the core of our engineering and consultancy services. We are committed to delivering solutions that not only drive progress but also ensure a positive impact on the environment. Our operations align with Saudi Vision 2030—a transformative national agenda that prioritizes sustainability, clean energy, and environmental stewardship.\r\n\r\n",
                Image = "ourMission.webp",
                SectionId = 2,
                PageType = PageType.Sustinabilty
            };

            var team2_2 = new Article
            {
                Title = "Our Commitment to a Sustainable Future\r\n",
                Description = "",
                Image = "ourMission.webp",
                SectionId = 3,
                PageType = PageType.Sustinabilty
            };
            var team2_3 = new Article
            {
                Title = "Our Role in Saudi Vision 2030\r\n",
                Description = "In line with the Kingdom’s sustainability goals, SEGI contributes to: Reducing Carbon Emissions: By enhancing the energy efficiency of systems through studies like Reactive Power Assessments and Arc Flash Hazard Analyses, we ensure that industrial projects operate with minimal energy loss and emissions. Optimizing Infrastructure Development: Our Stress Analysis Studies and Cathodic Protection Solutionsensure long-term infrastructure reliability while reducing maintenance and material wastage. Sustainable Water Solutions: SEGI's expertise in hydraulic studies supports the development of sustainable water systems that preserve this vital resource. Compliance & Innovation: Ensuring projects adhere to environmental regulations while applying cutting-edge tools and techniques to support sustainable outcomes.\r\n\r\n",
                Image = "ourMission.webp",
                SectionId = 4,
                PageType = PageType.Sustinabilty
            };
            var team2_4 = new Article
            {
                Title = "Our Vision for a Greener Tomorrow\r\n",
                Description = "Guiding principles:-\r\nSEGI believes in creating a balance between industrial growth and environmental preservation. By leveraging our engineering expertise, we design and execute projects that improve efficiency, reduce waste, and promote environmental well-being—all while aligning with the Kingdom’s long-term vision for sustainability.\r\n\r\n",
                Image = "ourMission.webp",
                SectionId = 5,
                PageType = PageType.Sustinabilty
            };
            var team2_5 = new Article
            {
                Title = "Partner with SEGI for Sustainable Solutions\r\n",
                Description = "SEGI is committed to engineering a future where progress and sustainability go hand in hand. Let us partner with you to deliver projects that are efficient, resilient, and environmentally responsible.\r\n\r\n",
                Image = "ourMission.webp",
                SectionId = 6,
                PageType = PageType.Sustinabilty
            };
            var team3_1 = new Article
            {
                Title = "Career",
                Description = "",
                Image = "ourMission.webp",
                SectionId = 1,
                PageType = PageType.Career
            };
            var team4_1 = new Article
            {
                Title = "Project",
                Description = "",
                Image = "ourMission.webp",
                SectionId = 1,
                PageType = PageType.Project
            };
            var team5_1 = new Article
            {
                Title = "History",
                Description = "",
                Image = "ourMission.webp",
                SectionId = 1,
                PageType = PageType.History
            };
            var team6_1 = new Article
            {
                Title = "Contact Us",
                Description = "If we can help you with your existing or future projects or you would like to find out more about SEGI, please leave us a message using the form below and one of our support team will contact you as soon as possible. Alternatively you can find direct contact details for any global locations by using the find an office drop down below\r\n",
                Image = "ourMission.webp",
                SectionId = 1,
                PageType = PageType.ContactUs
            };
            context.Articles.AddRange(team1, team2, team3, team4, team5, team6, team7, team9, team10, team11, team12, team13, team14, team15, team16, team17, team2_1, team2_2, team2_3, team2_4, team2_5, team3_1, team4_1, team5_1, team6_1);
            context.SaveChanges();

        }
        public static void SeedGeneralEngineeringServicesItem(this ApplicationDbContext context)
        {
            if (context.GeneralEngineeringServicesItems.Any())
            {
                return;
            }
            var sector1 = new GeneralEngineeringServicesItem
            {
                Title = "Engineering Design\r\n",
                Description = "End-to-end design solutions across multiple disciplines to meet project specifications. ",
                Image = "news.webp",
            };
            var sector2 = new GeneralEngineeringServicesItem
            {
                Title = "Project Management Services",
                Description = "Effective planning, execution, and delivery of projects, ensuring quality and timely completion.",
                Image = "news.webp",
            };
            var sector3 = new GeneralEngineeringServicesItem
            {
                Title = "Procurement & Material Support",
                Description = "Effective planning, execution, and delivery of projects, ensuring quality and timely completion.",
                Image = "news.webp",

            };
            var sector4 = new GeneralEngineeringServicesItem
            {
                Title = "Pre-Bid Assessment and Support",
                Description = "Assisting clients with strategic pre-bid evaluations to ensure competitive proposals. ",
                Image = "news.webp",
            };
            var sector5 = new GeneralEngineeringServicesItem
            {
                Title = "Water Utilities",
                Description = "All Types of Power Plants\nSubstations from 33- 380 Kv.\nTransmission Lines and Underground",
                Image = "news.webp",

            };
            var sector6 = new GeneralEngineeringServicesItem
            {
                Title = "Planning and Scheduling",
                Description = "Comprehensive project scheduling to optimize time and resource allocation.",
                Image = "news.webp",

            };
            var sector7 = new GeneralEngineeringServicesItem
            {
                Title = "Project Developmen",
                Description = "Expertise in all phases of development:\r\nFeasibility Studies, DBSP, Pre-FEED, FEED, Detailed Design, and Shop Drawings.",
                Image = "news.webp",

            };
            var sector8 = new GeneralEngineeringServicesItem
            {
                Title = "Design and Peer Review",
                Description = "Thorough reviews in line with client standards and specifications to ensure quality and compliance.",
                Image = "news.webp",

            };
            context.GeneralEngineeringServicesItems.AddRange(sector1, sector2, sector3, sector4, sector5, sector6, sector7, sector8);
            context.SaveChanges();

        }
        public static void SeedPMCServicesItems(this ApplicationDbContext context)
        {
            if (context.PMCServicesItems.Any())
            {
                return;
            }
            var sector1 = new PMCServicesItem
            {
                Title = "Project Management",
                Description = "Complete oversight of project implementation to ensure objectives are delivered on time, within budget, and to the highest quality ",
                Image = "news.webp",
            };
            var sector2 = new PMCServicesItem
            {
                Title = "Construction Management",
                Description = "Strategic supervision and coordination of construction activities to maintain timelines, safety, and cost efficiency.",
                Image = "news.webp",
            };
            var sector3 = new PMCServicesItem
            {
                Title = "Site Supervision & Inspection",
                Description = "On-site management and inspections to ensure construction aligns with design specifications and industry standards. ",
                Image = "news.webp",

            };
            var sector4 = new PMCServicesItem
            {
                Title = "Design Review",
                Description = "Comprehensive review of designs to ensure feasibility, compliance, and alignment with client goals.",
                Image = "news.webp",
            };
            var sector5 = new PMCServicesItem
            {
                Title = "Planning and Scheduling",
                Description = "Developing detailed project timelines and schedules to drive timely execution and progress tracking.",
                Image = "news.webp",

            };
            var sector6 = new PMCServicesItem
            {
                Title = "Safety Analysis and Implementation",
                Description = "Conducting rigorous safety assessments and implementing safety measures to minimize risks and ensure a secure working environment.",
                Image = "news.webp",

            };
            var sector7 = new PMCServicesItem
            {
                Title = "Project Interface Coordination",
                Description = "Managing communication and coordination between stakeholders, contractors, and suppliers to maintain seamless project flow.",
                Image = "news.webp",

            };
            var sector8 = new PMCServicesItem
            {
                Title = "Final Acceptance Testing",
                Description = "Overseeing and verifying project deliverables through final testing and acceptance protocols.",
                Image = "news.webp",

            };
            context.PMCServicesItems.AddRange(sector1, sector2, sector3, sector4, sector5, sector6, sector7, sector8);
            context.SaveChanges();

        }
        public static void SeedProcessSafetyStudies(this ApplicationDbContext context)
        {
            if (context.ProcessSafetyStudiess.Any())
            {
                return;
            }
            context.ProcessSafetyStudiess.AddRange(new ProcessSafetyStudies
            {
                Title = "Process Safety Studies\r\n",
                Description = "",
                Image = "news.webp",
            });
            context.SaveChanges();

        }
        public static void SeedProcessSafetyStudiesItems(this ApplicationDbContext context)
        {
            if (context.ProcessSafetyStudiesItems.Any())
            {
                return;
            }
            var sector1 = new ProcessSafetyStudiesItem
            {
                Title = "PHA (Process Hazard Analysis):\r\n",
                Description = "Identifying and mitigating process risks.\r\n\r\n",
            };
            var sector2 = new ProcessSafetyStudiesItem
            {
                Title = "HAZOP (Hazard and Operability Studies):\r\n",
                Description = "Systematic analysis to detect deviations and ensure operational safety.\r\n\r\n",
            };
            var sector3 = new ProcessSafetyStudiesItem
            {
                Title = "SIL (Safety Integrity Level Assessment):\r\n",
                Description = "Maintaining open, clear, and honest communication with all stakeholders.\r\n\r\n",

            };
            var sector4 = new ProcessSafetyStudiesItem
            {
                Title = "HAZID (Hazard Identification):\r\n",
                Description = "Proactively recognizing hazards for risk mitigation.\r\n\r\n",
            };

            context.ProcessSafetyStudiesItems.AddRange(sector1, sector2, sector3, sector4);
            context.SaveChanges();

        }
        public static void SeedCategories(this ApplicationDbContext context)
        {
            if (context.Categories.Any())
            {
                return;
            }
            var item1 = new Category
            {
                Name = "Category 1",
            };
            var item2 = new Category
            {
                Name = "Category 2",
            };
            var item3 = new Category
            {
                Name = "Category 3",
            };

            context.Categories.AddRange(item1, item2, item3);
            context.SaveChanges();

        }
        public static void SeedNews(this ApplicationDbContext context)
        {
            if (context.News.Any())
            {
                return;
            }
            var item1 = new New
            {
                Title = "Engineering Design\r\n",
                Description = "End-to-end design solutions across multiple disciplines to meet project specifications. ",
                Short_Description = "SEGI Secures Contract for Detailed Engineering of the UK’s First Large-Scale Low Carbon Hydrogen Project\r\nSEGI to deliver detailed engineering of the low carbon hydrogen production plant (HPP1) at the Stanlow Manufacturing Complex in Ellesmere Port, Cheshire",
                Image = "news.webp",
                Image_Cover = "news.webp",
                NewCategories = new List<NewCategory> { new NewCategory { CategoryId = 1 } }
            };
            var item2 = new New
            {
                Title = "Engineering Design\r\n",
                Description = "End-to-end design solutions across multiple disciplines to meet project specifications. ",
                Short_Description = "SEGI Secures Contract for Detailed Engineering of the UK’s First Large-Scale Low Carbon Hydrogen Project\r\nSEGI to deliver detailed engineering of the low carbon hydrogen production plant (HPP1) at the Stanlow Manufacturing Complex in Ellesmere Port, Cheshire",
                Image = "news.webp",
                Image_Cover = "news.webp",
                NewCategories = new List<NewCategory> { new NewCategory { CategoryId = 2 } }

            };
            var item3 = new New
            {
                Title = "Engineering Design\r\n",
                Description = "End-to-end design solutions across multiple disciplines to meet project specifications. ",
                Short_Description = "SEGI Secures Contract for Detailed Engineering of the UK’s First Large-Scale Low Carbon Hydrogen Project\r\nSEGI to deliver detailed engineering of the low carbon hydrogen production plant (HPP1) at the Stanlow Manufacturing Complex in Ellesmere Port, Cheshire",
                Image = "news.webp",
                Image_Cover = "news.webp",
                NewCategories = new List<NewCategory> { new NewCategory { CategoryId = 3 } }

            };

            context.News.AddRange(item1, item2, item3);
            context.SaveChanges();

        }
        public static void SeedHistory(this ApplicationDbContext context)
        {
            if (context.Histories.Any())
            {
                return;
            }
            var history1 = new History
            {
                Description = "SEGI has more than 1,000 engineering workforce and recently opened additional engineering design office in Al Khobar to sustain its growing engineering workforce and provide services to more clients in the Kingdom of Saudi Arabia.\r\n\r\n",
                Image = "news.webp",
                Case = "Currently",
                IsActive = true,
            };
            var history2 = new History
            {
                Description = "Consolidated all its entities and engineering capabilities under one professional partnership that is 100% Saudi owned.\r\n\r\n",
                Image = "news.webp",
                Year = 2022,
                Case = "",
                IsActive = false,


            };
            var history3 = new History
            {
                Description = "Formed a mixed professional partnership with Amec Foster Wheeler (AFWAPFC) based in Al Khobar to offering consultancy, engineering and project management services in Saudi Arabia.\r\n\r\n ",
                Image = "news.webp",
                Year = 2014,
                Case = "",
                IsActive = false,


            };
            var history4 = new History
            {
                Description = "Opened its regional offices in Jeddah, Riyadh and Abha and representation offices in India and Philippines. SEGI became one of the largest engineering and PMC consultant in Saudi Arabia.\r\n\r\n",
                Image = "news.webp",
                Year = 2007,
                Case = "",
                IsActive = false,
            };
            context.Histories.AddRange(history1, history2, history3, history4);
            context.SaveChanges();

        }
        public static void SeedBannerMain(this ApplicationDbContext context)
        {
            if (context.BannerMains.Any())
            {
                return;
            }

            var model1 = new BannerMain
            {
                Content = "",
                BannerContentType = BannerContentType.Image,
                Image = "ourMission.webp",
                BannerPageType = BannerPageType.Home
            };
            var model2 = new BannerMain
            {
                Content = "",
                BannerContentType = BannerContentType.Image,
                Image = "ourMission.webp",
                BannerPageType = BannerPageType.Sustinabilty
            };


            context.BannerMains.AddRange(model1, model2);
            context.SaveChanges();

        }
        public static void SeedFixedItem(this ApplicationDbContext context)
        {
            if (context.FixedItems.Any())
            {
                return;
            }
            var item1 = new FixedItem
            {
                Email = "segi@gmai.com",
                Location = "Sudia Arabia USA",
                Logo_Dark = null,
                Logo_White = null,
                X = null,
                FaceBook = null,
                Youtube = null,
                LinkedIn = null,
                TikTok = null,
                Snapchat = null,
                Instagram = null,
                Phone = "+96667673632"
            };


            context.FixedItems.AddRange(item1);
            context.SaveChanges();

        }

        public static async Task SeedUsers(this UserManager<ApplicationUser> userManager)
        {
            if (await userManager.Users.AnyAsync()) return;

            var user_SuperAdmin = new ApplicationUser
            {

                FullName = "SuperAdmin",
                CreatedAt = DateTime.Now,
                UserName = "Superadmin@SEGI.com",
                Email = "Superadmin@SEGI.com",
                IsDelete = false,
                Code = null,
                EmailConfirmed = true,
                ImageUrl = "",
                UserType = UserType.SuperAdmin,
                PhoneNumber = "05111111111"
            };

            var user_user = new ApplicationUser
            {
                FullName = "User",
                CreatedAt = DateTime.Now,
                UserName = "user@SEGI.com",
                Email = "user@SEGI.com",
                IsDelete = false,
                Code = null,
                EmailConfirmed = true,
                ImageUrl = "",
                UserType = UserType.User,
                PhoneNumber = "05111111111"

            };

            await userManager.CreateAsync(user_SuperAdmin, "123321");
            await userManager.CreateAsync(user_user, "123321");

            await userManager.AddToRoleAsync(user_SuperAdmin, Constant.RolesFilter.SuperAdmin);
            await userManager.AddToRoleAsync(user_user, (Constant.RolesFilter.User));

        }

    }
}
