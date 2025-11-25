using Bloggy.Application.Persistense;
using Bloggy.Domain.Entites;
using Bloggy.Infrastructure;
using Bloggy.Infrastructure.Persistense;
using Bloggy.Infrastructure.Services.MeiliSearch;
using Microsoft.OpenApi.Models;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(
                    "v1",
                    new OpenApiInfo
                    {
                        Title = "Bloggy",
                        Version = "v1"
                    }
                );

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                var securityRequirement = new OpenApiSecurityRequirement
                {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
                };

                options.AddSecurityRequirement(securityRequirement);
            });

            builder.Services.AddSingleton(serviceProvider =>
            {
                var configuration = builder.Configuration;
                // var meiliSearchUrl = configuration["MeiliSearch:Url"];
                // var apiKey = configuration["MeiliSearch:ApiKey"];

                var meiliSearchUrl = configuration["MeiliSearch:Url"]
                ?? throw new InvalidOperationException("MeiliSearch:Url is not configured");
                var apiKey = configuration["MeiliSearch:ApiKey"]
                ?? throw new InvalidOperationException("MeiliSearch:ApiKey is not configured");

                return new MeiliSearchService(meiliSearchUrl, apiKey); // Pass configuration into the service
            });

            builder.Services.AddScoped<IMeiliSyncService, MeiliSyncService>();

            builder.Services.AddControllers();

            builder.Services.AddHttpContextAccessor();
            builder.Services
                .AddApplication()
                .AddInfrastructure(builder.Configuration);
        }
        var app = builder.Build();
        {
            app.UseExceptionHandler("/error");
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                var appDbContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();
                appDbContext.Topics.AddRange(
                    new Topic { Id = 1, Name = "Gaming" },
                    new Topic { Id = 2, Name = "Sports" },
                    new Topic { Id = 3, Name = "Business" },
                    new Topic { Id = 4, Name = "Crypto" },
                    new Topic { Id = 5, Name = "Television" },
                    new Topic { Id = 6, Name = "Celebrity" },
                    new Topic { Id = 7, Name = "Animals and Pets" },
                    new Topic { Id = 8, Name = "Anime" },
                    new Topic { Id = 9, Name = "Art" },
                    new Topic { Id = 10, Name = "Cars and Motor Vehicles" },
                    new Topic { Id = 11, Name = "Crafts and DIY" },
                    new Topic { Id = 12, Name = "Culture, Race, and Ethnicity" },
                    new Topic { Id = 13, Name = "Ethics and Philosophy" },
                    new Topic { Id = 14, Name = "Fashion" },
                    new Topic { Id = 15, Name = "Food and Drink" },
                    new Topic { Id = 16, Name = "History" },
                    new Topic { Id = 17, Name = "Hobbies" },
                    new Topic { Id = 18, Name = "Law" },
                    new Topic { Id = 19, Name = "Learning and Education" },
                    new Topic { Id = 20, Name = "Military" },
                    new Topic { Id = 21, Name = "Movies" },
                    new Topic { Id = 22, Name = "Music" },
                    new Topic { Id = 23, Name = "Place" },
                    new Topic { Id = 24, Name = "Podcasts and Streamers" },
                    new Topic { Id = 25, Name = "Politics" },
                    new Topic { Id = 26, Name = "Programming" },
                    new Topic { Id = 27, Name = "Reading, Writing and Literature" },
                    new Topic { Id = 28, Name = "Relogion and Spirituality" },
                    new Topic { Id = 29, Name = "Science" },
                    new Topic { Id = 30, Name = "Tabletop Games" },
                    new Topic { Id = 31, Name = "Technology" },
                    new Topic { Id = 32, Name = "Teacnology" },
                    new Topic { Id = 33, Name = "Travel" }
                );
                appDbContext.SaveChanges();
                var user = new User
                {
                    Name = "Test",
                    Email = "test@mail.com",
                    Password = "test"
                };
                appDbContext.Users.Add(user);
                appDbContext.SaveChanges();
                appDbContext.Posts.AddRange(
                    new Post
                    {
                        AuthorId = user.Id,
                        Title = "Lorem Ipsum",
                        ImageUri = "http://localhost:5010/images/1.jpg",
                        Description = """
Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam metus nibh, volutpat id tellus non, imperdiet vehicula sapien.
Vivamus venenatis cursus mi, vitae tincidunt sapien tempus vitae. Donec pellentesque efficitur ex quis posuere.
Donec bibendum massa eu sapien scelerisque tristique. Curabitur in semper libero, at ultrices tortor.
Vestibulum consequat ligula felis, sit amet sodales nibh lobortis sed.
Duis porta, nisi non aliquam facilisis, arcu ante sollicitudin ligula, ut hendrerit lorem lorem id ex.
Donec at accumsan ipsum. Proin nisl quam, tincidunt ornare cursus ac, imperdiet id lectus. Aenean blandit odio at cursus semper.
Ut tristique mollis tempor. Nam iaculis augue sed elementum feugiat. Donec tristique ut nulla laoreet tristique.

Duis varius neque vitae velit dapibus, eu eleifend quam pulvinar. Nam dictum mi eu justo pellentesque sagittis.
Aenean convallis nunc non ipsum facilisis, ut convallis enim vulputate. Nullam sed risus a risus porttitor ultricies.
In hac habitasse platea dictumst. Etiam vel condimentum lectus. Vestibulum ut leo id massa egestas finibus.
Suspendisse dui erat, cursus sed suscipit et, pulvinar sed purus. In et elementum felis. Vivamus cursus tincidunt fermentum.
Integer tellus lacus, cursus vitae vulputate eu, volutpat ut nisi.
Integer luctus, arcu a finibus pulvinar, augue ante pellentesque diam, et vulputate nisl ligula at eros.
Aenean condimentum nunc sem, porttitor elementum metus blandit sit amet.
Pellentesque quam ante, efficitur quis mi ut, sollicitudin dignissim orci.

Morbi mattis tristique purus, non commodo orci iaculis sed.
Nullam tempus odio vitae massa volutpat, et accumsan purus lacinia. Morbi faucibus at arcu vehicula accumsan.
Mauris at arcu ac metus bibendum maximus. In sollicitudin nec lectus et consequat. Vivamus nec congue sem.
Nullam vulputate, mauris et sollicitudin hendrerit, tellus nunc gravida purus, in pulvinar felis felis et mauris.
Maecenas ornare nisl turpis. Aliquam id sem odio. In nec eros et lectus vestibulum posuere sed vel leo.

Aliquam dapibus, risus at venenatis tincidunt, metus risus interdum sapien, at sodales odio sapien quis lectus.
Etiam est risus, egestas vel dui id, imperdiet varius dui. Nulla a facilisis tortor, nec tempor erat.
Suspendisse porttitor est quis ipsum dictum finibus. Sed blandit suscipit magna eu rhoncus.
Etiam ante risus, dapibus non mi et, feugiat bibendum sapien. Etiam consequat risus at nisl congue feugiat.
Aliquam sit amet porttitor augue. Nulla odio velit, aliquet et tincidunt eu, porttitor sit amet mi.
Duis sodales porttitor mauris, quis pharetra nulla ultrices dignissim. Vivamus eleifend urna vel cursus laoreet.
Donec tortor enim, sodales id imperdiet iaculis, egestas id sem. Praesent a interdum nunc.

Donec aliquam neque sed urna malesuada pellentesque commodo in diam.
Aenean ligula sem, euismod non scelerisque quis, suscipit laoreet massa.
Nullam tellus velit, accumsan eu ex suscipit, consectetur eleifend est. Nam elementum dapibus condimentum.
Pellentesque aliquet mi vitae mauris cursus rutrum. Aliquam pellentesque maximus congue.
Vestibulum enim ipsum, mollis malesuada semper id, placerat eu nulla. Vestibulum finibus dui purus, at varius libero mollis ac.
Aliquam tempus ante ac tellus imperdiet luctus. Maecenas et suscipit erat. Morbi venenatis consequat arcu a commodo.
""",
                        Topics = appDbContext.Topics.Where(t => t.Id == 1).ToList()
                    },
                    new Post
                    {
                        AuthorId = user.Id,
                        Title = "Slow Rollout of National Charging System Could Hinder E.V. Adoption",
                        ImageUri = "http://localhost:5010/images/2.webp",
                        Description = """
Lawmakers approved $5 billion for states to build a network of fast chargers two years ago.
Although some states have made progress in recent weeks, most have not yet awarded contracts or started construction.
""",
                        Topics = appDbContext.Topics.Where(t => t.Id == 25).ToList()
                    },
                    new Post
                    {
                        AuthorId = user.Id,
                        Title = "The Best To-Do List App",
                        ImageUri = "http://localhost:5010/images/3.webp",
                        Description = """
Mastering your to-do list can seem like a Sisyphean task, but a good to-do list app should help
you regain control over your routines and make it possible to keep chaos at bay.

Our to-do list app picks, Todoist, TickTick, and the Apple-exclusive Things 3, are a breeze to use,
have thoughtful designs, and feature flexible organization schemes, so you can conveniently hop in,
address your obligations, and enter new tasks—then get right back to the doing.
""",
                        Topics = appDbContext.Topics.Where(t => t.Id == 25).ToList()
                    },
                    new Post
                    {
                        AuthorId = user.Id,
                        Title = "No Oversight: Inside a Boom-Time Start-Up Fraud and Its Unraveling",
                        ImageUri = "http://localhost:5010/images/4.webp",
                        Description = """
False claims and risky trades at the Silicon Valley start-up HeadSpin were part
of a pattern of trouble emerging at young companies that lacked controls.
""",
                        Topics = appDbContext.Topics.Where(t => t.Id == 25).ToList()
                    },
                    new Post
                    {
                        AuthorId = user.Id,
                        Title = "Apple Explores A.I. Deals With News Publishers",
                        ImageUri = "http://localhost:5010/images/5.webp",
                        Description = """
The company has discussed multiyear deals worth at least $50 million to train
its generative A.I. systems on publishers’ news articles.

Apple has opened negotiations in recent weeks with major news and publishing organizations,
seeking permission to use their material in the company’s development of generative artificial
intelligence systems, according to four people familiar with the discussions.

The technology giant has floated multiyear deals worth at least $50 million to license the archives
of news articles, said the people with knowledge of talks, who spoke on the condition of anonymity
to discuss sensitive negotiations. The news organizations contacted by Apple include Condé Nast,
publisher of Vogue and The New Yorker; NBC News; and IAC, which owns People,
The Daily Beast and Better Homes and Gardens.
""",
                        Topics = appDbContext.Topics.Where(t => t.Id == 25).ToList()
                    },
                    new Post
                    {
                        AuthorId = user.Id,
                        Title = "Apple Explores A.I. Deals With News Publishers",
                        ImageUri = "http://localhost:5010/images/5.webp",
                        Description = """
The company has discussed multiyear deals worth at least $50 million to train
its generative A.I. systems on publishers’ news articles.

Apple has opened negotiations in recent weeks with major news and publishing organizations,
seeking permission to use their material in the company’s development of generative artificial
intelligence systems, according to four people familiar with the discussions.

The technology giant has floated multiyear deals worth at least $50 million to license the archives
of news articles, said the people with knowledge of talks, who spoke on the condition of anonymity
to discuss sensitive negotiations. The news organizations contacted by Apple include Condé Nast,
publisher of Vogue and The New Yorker; NBC News; and IAC, which owns People,
The Daily Beast and Better Homes and Gardens.
""",
                        Topics = appDbContext.Topics.Where(t => t.Id == 25).ToList()
                    },
                    new Post
                    {
                        AuthorId = user.Id,
                        Title = "Slow Rollout of National Charging System Could Hinder E.V. Adoption",
                        ImageUri = "http://localhost:5010/images/2.webp",
                        Description = """
Lawmakers approved $5 billion for states to build a network of fast chargers two years ago.
Although some states have made progress in recent weeks, most have not yet awarded contracts or started construction.
""",
                        Topics = appDbContext.Topics.Where(t => t.Id == 25).ToList()
                    },
                    new Post
                    {
                        AuthorId = user.Id,
                        Title = "Balancing spirit and academic traditions",
                        ImageUri = "http://localhost:5010/images/2.webp",
                        Description = """
For good reasons, we believe that church-related beliefs should not influence public education, unless the beliefs are part of a curriculum that involves the history or comparison of religions. With that in mind, and with our emphasis on educating the individual, it seems that concepts involving spirit are off limits in our schools. Similarly, in India, the cradle of so many spiritual traditions, there is a national interest in Values Education and an abhorrence of such a curriculum coming from any of India's many spiritual traditions. Colonial influences have left India as secular about education as the United States. This becomes important when we grasp that Values Education is a secular frame for millennia of spiritual traditions. The world's first cultures see the environmental and cultural genocide by colonial and post-colonial “commercial”, evangelical, and industrial countries as symptomatic of a lack of spirit, a disharmony with the beauty, balance and consciousness of the earth. Reasonably, they suggest that our environmental 
and related crises are caused by human disharmony, or a lack of spirit, sensitivity, or we might say sympathetic, intuitive, and aesthetic resonance with the creation. Yet, ironically, our schools are all about spirit rallies. In these exercises of team spirit we use sports to light the torch of a collective belief and passion similar to nationalism during wartime. Thus, we might hypothesise that inspiring spirit, a collective purpose that inspires deep enthusiasm, in the service of sports is a healthy secular activity. But inspiring spirit, as a collective, unifying concept for the care of one another and the earth is not. It's time we took another look at the relationship between secular disciplines and spiritual teachings. While I'd argue for keeping specific religious teachings out of our schools, in the name of psychology, anthropology, comparative religions, team work, and shared purpose, I'd argue that to be fully human, which may be one good definition of being fully educated, people need to understand the towering spiritual traditions that teach to the heart of our humanity. Teaching such a balanced curriculum should NOT preach beliefs that come from any specific spiritual tradition. Indeed, by comparing these traditions, we can learn the common themes and divergences. We can also learn that many great people who were not within any religious tradition, held values in common with believers. Good and great people come clothed in widely varying spiritual and secular robes. Education should not be about maintaining ignorance about a core element in our nature--our joy in joining fully in events that quicken our sense of spirit. If people are not educated to the dangerous excesses of belief and spirit, as manipulated by authoritarians, then a secular education may leave people lonely and vulnerable rather than united and empowered to sympathetically serve our collective values, which are so similar across all spiritual and humanistic traditions.
""",
                        Topics = appDbContext.Topics.Where(t => t.Id == 25).ToList()
                    },
                    new Post
                    {
                        AuthorId = user.Id,
                        Title = "Unity in Values Education",
                        ImageUri = "http://localhost:5010/images/2.webp",
                        Description = """
Many people experience our unity with God and thus believe that a spiritual basis for Values Education (VE) is important. This paper suggests that a sense of unity can be achieved through cosmology, which means we can use science and the humanities to educate for a sense of unity. Science tells us that the universe is a unity of energy in time and perhaps beyond time. For example, all the water on earth, like all the elements and many molecules that make earth and life possible, came from a condensing cloud of fire and stardust. In all its forms, the core of all the matter on earth was created billions of years ago in dying stars. So the life-enabling water in our bodies and most if not all of the atoms is older than the earth. Every drop in oceans, rivers, lakes, and drinking fountains was born from cosmic explosions, as were all the elements in the periodic table. Thus, whether in the beginning was the word, or the thought of God, and we are created from God's consciousness, or whether in the beginning was cosmic energy, which by accident formed stars and made all life possible, either way, our existence is an unfathomable miracle, a great wonder, and our consciousness, at its best, is enchanted by our gift of being, whether that gift is gift-wrapped by God or a beautiful accident. Either way, here we are and we want to make the very best of it. We want to celebrate being alive and being with one another. And we want to celebrate the knowledge and wisdom that our ancestors have gifted to us through words, tools, attitudes and habits, passed like the first fire from generation to generation. The important question for VE is not whether we find our morality in unity with God's, for even if there were no God, there would still be unity. The unity would be in the fact that we are all the same miracles of time, sun, matter, and DNA, of tenderness and language. We all want to be cared for, respected and loved. We fear hunger, deceit, loneliness, and death. We all respond to kindness unless somehow we've been terribly wounded. We love to chatter with friends, to laugh, to see courage, kindness and beauty. So let us agree that God is NOT essential for everyone to justify Values Education. After all, many atheists are profoundly moral and many religious people have harmful values. Even if their God says thou shall not kill, they will stone their child if that child breaks a commandment, and they will say it is holy work to kill another who worships the same God, but in a slightly different way. This is not to suggest that God is not present and that God's presence is not the source of our quest for values. Let's simply agree that we do not have to find our unifying theme in God. It is enough to know that we all are of the same creation; made from the same energy, have the same hopes and fears, and all hunger for recognition, kindness, support, fun and tenderness. Thus, we can say that the basis of VE is our unity with each other and with each grain of sand, seed, flower, bee, tear, sunbeam and smile. All exist, products of one-cosmology, one-galaxy and solar system, one-biology and ecology, one-social and cultural continuum. That is a great deal to have in common. Once we sense our unity, then we are drawn to treat the world and others as we wish to be treated. But to be consistently kind, we must develop our awareness by observing that many times our minds and hearts are divided and we are not kind to others or ourselves. 
We are ambivalent or worse—very judgmental, very cruel. We gossip and bully, ignore, threaten and hurt others. What we fail to see is that our cruelty, our judgmental thinking and behavior then casts a rejecting, angry shadow back on us, or actually originates within a rejecting shadow within us, a shadow that we carry. This is why both spiritual and secular traditions in religion and in psychology teach us to witness or be mindful without judging. We are advised to heal the division in our minds and hearts, to heal the divisiveness in our attitudes so we will NOT nourish this fragmentation, and then unconsciously turn it against ourselves. In mindfulness or witnessing we are taught to let our minds become like a clear stream, to let all our thoughts and feelings be like fish that swim through. Just as the stream does NOT hold onto the fish, so our egos, or our consciousness, should NOT hold on to what is good or bad because if we hold on, then the mind is stopped from the perfect stillness that flows, and we stop growing, stop expanding our awareness, stop achieving an ever more subtle appreciation for the creation that flows within us and outside of us—making us and every moment possible. Thus, the important thing for VE may not be a unity based on spiritual beliefs, but simply a sense of the unity of creation, and our gift of awareness that enables us to know that unity, and to then feel our responsibility for the creation and for each of the creatures and things in it. Nature is the unity in the world from which life springs, so when we let ourselves be at one with nature, we sense this unity and we sense our instinct to be stewards, caretakers of nature, just as we have an instinct to be caretakers for one another. The challenge of living in such unity is to accept ourselves with all of our fears, impulses, destructive and selfish habits. We must learn to see that we make ourselves superior by making someone else inferior. We are better because we are richer, stronger, smarter, taller, tougher, smaller, prettier, younger or older. All these ideas of specialness hurt us because they divide us first within ourselves, and then from others. In this division our insecurity is expressed, and so too our careless, thoughtless, and destructive behavior—as if we want to prove that we don't have to care because if we care then we too are vulnerable and must have compassion born of not yet realized self-acceptance or self-love. Our secular world's emphasis on specialness is the opposite of cultivating humility. For it is in being special like everyone else, or in our commonness, in our sense of community, in our caring and communication, that we find our unity. The seed of our most caring consciousness is born in our sense of others, and of equality. This has profound implications for how we design and explore all learning. We see that we do not want to divide or fragment people as we teach for assessments. This of course occurs when students study for competitive tests. Further, we do not want to divide the world into disconnected subjects and facts, because such division destroys the teacher's and learner's sense of unity that spurs curiosity, exploration, and wonder. Instead, we want to discover how all things are related, interacting, sustaining, and creating. Hummingbirds, snakes, snails, and whales, we need to know that we all sip the same ancient water and inhale the same leaf-green air while living our shared DNA guided lives. We are naturally curious and each sincerely asked question nourishes new questions. We are greatly enriched when we discover the miracles of how water and oxygen are formed and forever refreshed, how butterflies, salmon, lobsters, and swans migrate, and how DNA changes gills into lungs, scales into feathers and skin. We also want to enable people to discover how the mind that is relaxed and focused sees clearly, so art, play, building, learning, communicating and caring, nourish compassion unified in the unfathomable, and very likely, Godly beauty of being.
""",
                        Topics = appDbContext.Topics.Where(t => t.Id == 25).ToList()
                    },
                    new Post
                    {
                        AuthorId = user.Id,
                        Title = "Balancing measurement and human development ",
                        ImageUri = "http://localhost:5010/images/2.webp",
                        Description = """
Political, philanthropic, and educational decision makers want to measure what software is used, and to what effect, to guide (control) technology purchases. But this common sense interest raises common sense concerns. We have observed unintended consequences: in the name of accountability, educational systems become more top-down, standardized, sterile, didactic, redundant and irrelevant. Since digital systems have the advantage (and disadvantage) of easily facilitating measurement of learning, there is a danger that this ability to generate use and effects data for decision makers may push us toward curriculum designs and learning experiences that are immediately measurable. Such a curriculum would continue the unfortunate tradition of covering content at the expense of discovering and applying powerful, fascinating, and beautiful ideas. By standardizing curriculum to focus on accountability, we often cause teachers and students to feel that they are the means to bureaucratic ends—with higher test scores reflecting a cost-benefit. But we know from experience, management, and learning sciences that people learn, work, problem solve, and grow most happily and efficiently when they are not subject to tasks that are purposefully aligned to performance reviews. The danger, then, is that education would become a series of smarter digital interactions, more efficient in conveying set information and providing practice in set thinking procedures to meet preset measurable objectives. Where the systems are digital and adaptive, we call them personalized. But such systems may further make knowledge more extrinsic, rather than intrinsic; devoid of a felt sense of purpose. The price we pay for our well-intentioned obsession with accountability is that education is reduced to consuming measurable modules. This exacts a price. We retreat from exploring and creating interactions that facilitate important dimensions for human development. These dimensions focus on self-knowledge and happiness—trust, curiosity, questioning, initiative, identity, purpose, ethical behavior, service, stewardship, self-expression, and collaboration. This kind of learning emerges from a felt sense of personal, social, economic, intellectual or aesthetic purpose. It improves as a function of community and transcends most textbooks or formal curricula, though textbooks and lectures can be integrated into such learner-focused experiences. A very different way of looking at digital systems has an enormous role to play in creating these kinds of personal, community, and project-based learning experiences, even though our science of measurement and accountability has not caught up with these more open and human approaches. This more openly personal and social alternative views the computer not as the medium of delivery, feedback, and accountability; instead, the digital systems become mediums for facilitating social interactions (matching people by compatibility or complementary skills), providing facile means for communication about ideas and curriculum designs; identifying problems and opportunities, and storing a working history. Where in one case the technology is used to close the system, in the other it is used to open it to human concerns and creativity. In time, this kind of open design and publishing medium naturally facilitates a focus on improvement, on adapting content presentations and interactions for ever more diverse populations of students and teachers. In a diverse and multicultural society like the United States, the value of facilitating these capacities is beyond the bounds of measurement. Digital textbooks transformed into adaptive learning systems are here to stay, but we definitely want to build complementary digital systems. One excellent way to do this is to use technology to support schools and classrooms as Research & Design Studios, as Labs and Learning Communities. Education within Learning & Design Communities is very different from education as pre-established/machine-adapted content and tested teaching and learning. Where technology is used to facilitate Community Design and Learning, we can facilitate the creative problem-solving and culture-making processes in people. Inquiry becomes purposeful, communication sensitive, efficient, aesthetically pleasing and powerful. The outcome is building something new and/or evolving something old. This is problem-solving by design and building. It is STEM and STEAM transformed from academically taught disciplines to relevant problem-solving that combines purposeful work and learning. When knowledge is applied to make the world a better place, at least for one other person, academics can become an important part of giving people an enhanced 
sense of power, pride and identity. When people gain the power of belonging to a compatible group with shared purpose and effort, the results for all involved increase dramatically. The challenge for our technology/education communities is to NOT cut off our human communities—which already are far too isolated from our schools. On the contrary, we have to discover how to use our technology to facilitate warmer, richer, more meaningful learning, work and design communities in and around our classrooms. This will benefit education, and it will benefit our communities whose at-heart purpose is the protection and development of the next generation. Education needs the same urgency that we see in ants carrying their eggs from a disturbed nest. We should work to create better fitting and inspiring mentor relationships (face-to-face and virtual, local and global) for students and teachers. We should employ our technology to build and facilitate ever more inspired and talented curriculum designs processes enhanced by cross-discipline teams. We should build an infrastructure for facilitating learning by working in apprenticeship situation and in service and stewardship teams in our communities. Great gains can also be made in research if we view technology's measurement capacities in system-wide ways. Instead of thinking in terms of the normal curve in response to manipulating one or a very few variables, we can think in terms of the diversity of students and the multiple factors affecting where they are on that curve. Thus we can greatly refine our definition of experimental and control groups, and how we analyze the data coming from each. This means that many researchers can use the same parameters (data sets) and we can begin to drill down to N of one understanding. Ultimately we will discover that we can design unobtrusive and transferable data gathering systems to support system-wide research that provides insights into the diversity of students' in terms of their learning within online and off-line contexts. One use of technology is not necessarily better than the other. The adaptive learning and other delivery mediums serve important purposes. But that delivery process, because it more readily fits accountability measures, should not outweigh using technology to deepen our design, research, and service communities that are rooted in human interactions that technology now can facilitate. Our research should shine light on how to refine both systems to meet people's deepest needs—the needs of different types of students and teachers at different stages, in different contexts, and learning for different places for different purposes. Ultimately, we need to act from the knowledge that there are two common cores. The current academic curriculum should be viewed as second, or as developing in parallel with the first common core, which is Self-Knowledge and Social Emotional Learning. Ironically, by placing academics ahead of self-knowledge, and even ignoring self-knowledge, we have put academic learning in jeopardy, particularly for our most at-risk and gifted students. Thus, our current curriculum is not very successful at inspiring students and teachers to realize the richness of their individual and collective histories and cultures or their uniquely unfolding selves. This means we should use technology to help us redesign the Academic Common Core in two ways. One by machine delivered interactions. The other by computer facilitated human interactions that promote both self and academic learning. In other words, technology will realize its potential when Learning, Design, and Research Communities organize stakeholders around continuously improving their learning through all kinds of technology and human mediated interactions. Self-knowledge, or The First Common Core, naturally has to be much more personalized. This is the Common Core of self-awareness, introspection, and self-regulation built from the idiosyncratic processes of discovering and expressing one's self. It develops through and expresses identity. It is well exemplified by people like Socrates, van Gogh, and Thoreau, and by our best business, science, political, spiritual and artistic leaders. Its principles are well documented in developmental psychology. These two systems, the Personal Introspective/Purposeful Social and Creative and the Academic/Technical are not separate. Each, if well designed, should nourish the other, and either without the other will perpetuate a dangerously failed education system. A sound-working hypothesis may be that when the Common Core reflects the best of our cultural knowledge, which is an amazing legacy, we will see a coming together between self-and cultural and academic knowledge. Then we will know that we are building a great curriculum—a great learning community for human, social and economic development. But the further apart self and academic-cultural knowledge remain, the more we know our educational designs are failing our human and societal potential, and that measuring what's most successful in a failing system perpetuates failure and unhappiness—especially if accountability measures are so sterile as to ignore teacher and learner purpose, identity, and happiness. Political, philanthropic, business, and administrative leaders will want to tread lightly when considering narrowing our human potential by reducing education to what publishers and technologists can monetize and the Internet can readily deliver, adapt and measure. They also will want to consider the exciting, subtle and profound ways to use technology to rebalance education by deepening the capacities and insights that technology can help us to facilitate in communities devoted to service, stewardship, design, problem solving, and research.
""",
                        Topics = appDbContext.Topics.Where(t => t.Id == 25).ToList()
                    },
                    new Post
                    {
                        AuthorId = user.Id,
                        Title = "Synergies between academic and holistic learning",
                        ImageUri = "http://localhost:5010/images/2.webp",
                        Description = """
Throughout history, wise individuals have taught that learning is about inspiring a passion, not merely filling a void. True education manifests as love, leading to community service, with self-knowledge, compassion, imagination, and agency as core capacities applied within a meaningful context that fosters just-in-context learning. Unfortunately, our narrow academic traditions, focused on measurable returns, have too often confined education to dehumanizing learning and assessment processes. However, the deepening synergy between learning and computer sciences offers a solution: integrating measurable education outcomes with holistic human development through AI-supported portfolio assessments. While linear digital systems will still facilitate alignment between curriculum and learning measurements, we no longer need to prioritize standardized curricula or summative accountability over self-knowledge, creativity, personal growth, service, and project-based learning. Using Portfolios for assessment, we can expand our learning and assessment model to provide both measurable content delivery and foster personal growth while capturing measurable growth in all areas. Technology should be harnessed to create community-based learning environments that complement and integrate existing curriculum areas. Classrooms should evolve into studios for learning, design, and assessment. Here, students and teachers can engage with other talented people in meaningful, purposeful experiences that merge personal development with academic achievement. By integrating learning and computer sciences, we can transcend the limitations of traditional education by supporting all forms of learning and using technology to unlock deep and unique human potentials through more sophisticated research and more engaging and creative assessment processes.
""",
                        Topics = appDbContext.Topics.Where(t => t.Id == 25).ToList()
                    },
                    new Post
                    {
                        AuthorId = user.Id,
                        Title = "The AI inflection point",
                        ImageUri = "http://localhost:5010/images/2.webp",
                        Description = """
As AI refines the Internet, education can integrate curriculum and assessment with learning and computer sciences. The AI-enhanced Internet can create a bottom-up and top-down, new technology and design infrastructure to personalize learning. Global learning cultures can enrich local, and local cultures can refine global. Crowd-sourced, continuously improving, integrated open-source curriculum and assessment resources can replace traditional ‘teach to the test’ products. As local schools become curriculum design and assessment studios (STEAM enterprises), resources and practices to meet diverse needs will evolve, and assessment will become problem-solving oriented and enriching. To initiate this, an education R&D enterprise needs to support curriculum curation through smart searches, bottom-up authoring with continuous improvement, assessment, and HR matching. We might focus on 4 tools: • Curriculum Curation / Search • Curriculum Design and Adaptation • Assessment • Local ⬄ Global Human Resource Learning Communities
Four Tools Described:
Curriculum Curation
An AI Education Search Engine will help educators curate developmentally sequenced curricula. It will suggest digital and non-digital local to global resources to enhance learning processes across and among disciplines. It will recommend learning support resources according to what is known about the learner and the local and global resources available to her. This technology will scaffold and integrate learning experiences and personalize it for students’ developmental needs and teachers’ teaching styles.
Curriculum Design and Adaptation
Combining the AI Education Search Engine with curriculum authoring tools will support developing crowd-sourced, continuously improving curricula that are based on original content and adaptations to meet diverse learner and teacher needs. Supported by ePortfolio learner assessments, curriculum design will be refined by feedback systems making design responsive to learner and facilitator needs.
Assessment
e-Portfolios will enable students to reflect on their engagement, successes, and challenges. This centers assessment on student agency (executive function) and provides personalized metrics. Formative self-assessment improves learning while generating data to measure growth. Portfolio information includes goals/purposes, progress, frustrations, successes, questions, next steps, help needed, and where to find help. ePortfolio assessments will be shared in a personalized support network. People and AI will annotate reports and provide encouragement and feedback. Always formative and summative, these assessments focus on learner insight, literacy, agency, initiative, and problem-solving. These processes bring SEL, STEM, STEAM, and PBL (Project-Based Learning) to life with actionable information to personalize the curriculum, learning processes, and social and learning interactions. Portfolio assessment ends the need for expensive, judgmental, summative assessments that homogenize and narrow the curriculum into one size fits all, siloed, sliced, and diced activities, even as rhetoric and ideals call for integrated, personalized learning.
Local to Global Human Resources Community
Everyone can be part of the Smart HR Pool and can play multiple roles in cross-age, cross-capacities, cross-maturity, and interest groups. Teachers and students can be matched for compatibility, and the success of these matchings can be tracked in e-Portfolios. Students are placed in fitting study, tutoring, club, and other groups to increase engagement, belonging, and success. These meetings can be recorded for quality and safety and analyzed by AI. Students and groups can use e-Portfolios to provide real-time feedback to improve interactions and offer suggestions.
""",
                        Topics = appDbContext.Topics.Where(t => t.Id == 25).ToList()
                    },
                    new Post
                    {
                        AuthorId = user.Id,
                        Title = "Creating a new ecology for learning",
                        ImageUri = "http://localhost:5010/images/2.webp",
                        Description = """
To date, scaling educational reform has failed because we have been limited by our beliefs, models, and technology. Thanks to the wealth of human, cultural, and technological resources, the 21st century can be about reinventing the culture of human development and learning so schooling becomes a joy rather than a chore for students and teachers. With a sufficiently vital focus on student and teacher diversity, STEAM has the potential to broaden and deepen our educational thinking, design, and practice in ways that positively change resource design, teaching, and learning. However, to implement STEAM well, we must move out of cubicles and into classrooms. We must open classrooms to communities of people & nature, and communities to classrooms. We must stop thinking institutionally and begin thinking culturally, developmentally, and psychologically. We must grasp that meeting people's social-emotional (SEL) needs is the first step to human development and learning. Since learning builds on background knowledge and opportunities for self-expression, we must discover where learners are and meet them there. A person who can't float is not ready to learn to swim. The kinds of changes required to inspire and make the kinds of changes called for in the educational system would be unthinkable without current technology and our current sciences of learning, personality, culture, management, family & community. So, let's build a 21st Century model for creating a new ecology for human development, schooling & learning. Let's imagine 4 pillars that will hold the ecology up. What we immediately see when we look at these 4 Pillars is that we have moved from either/or thinking to INCLUSIVE, and thinking. This is essential to build a complete model and pedagogy that is sensitive to diversity and the common and idiosyncratic dimensions of human development and learning. The 4 pillars: 1. Self-knowledge is the beginning and end of social-emotional learning, successful adjustment, productivity, and the pursuit of happiness. Self-knowledge is the common-core of identity and a royal road into the wonderfully weird world of diversity, learning, creativity, and productivity. 2. Family, community, natural, cultural, and expert resources are the matrix out of which self and all other knowledge grow. Expert resources are included because, while experts are part of communities and are carriers of cultural resources, they are leaders who inspire us for efficient learning and the development of identity. 3. Online cultural + community + design + research + feedback resources are included as parallel elements to #2 because without the internet and knowledge engineering, we would not be able to consider how to coordinate so many different elements. It is precisely our ability to engineer all the components in #2 that 
creates the potential for us to take community organization within a human, cultural, and natural resource environment to a new level that enables us to deploy all of these resources in the service of human development. 4. Flexible, personalized, developmentally sensitive common core curriculum is now possible if we realize the wealth of our human, cultural, and natural resources as the vectors for designing our new common core. This common core should focus on problem-solving that facilitates service and stewardship to our families, communities, and nature. Please note that the goal that was first in school reform, the academic common core, is now 4th, because developmentally this is where it belongs. Why? Because, in general, we grow cognitively healthy where we are socially and emotionally supported. The stimulation for that growth comes out of the natural and social matrix that nourishes us; it comes out of the ways in which the culture designs the knowledge-gaining experiences to make them accessible and meaningful. Assessment is not the goal of learning; productivity in creating, sharing, and problem-solving is what motivates us to learn and work. Assessment is simply a part of the work/learning process that functions best when it supports insight and refinement. Thus, we should be talking about personalized development rather than just personalized learning.
""",
                        Topics = appDbContext.Topics.Where(t => t.Id == 25).ToList()
                    },
                    new Post
                    {
                        AuthorId = user.Id,
                        Title = "Making sense of teaching Science and the Arts in a human context ",
                        ImageUri = "http://localhost:5010/images/2.webp",
                        Description = """
I was a very poor student. I was in the lowest reading group in a blue-collar community. My mother was a reading specialist, and my older brother was an academic genius. I was fast on the playground and slow in the classroom. Since I was born to Jewish parents at the beginning of WWII, it was painfully clear that the world was crazy. By age eleven, I knew I wanted to help heal our hurting world. When I heard Freud knew why the world was crazy, I wanted to become a psychoanalyst. Instead, I became a psychotherapist and studied community mental health, curriculum design, and English.
By accident, while in graduate school at UCLA, I wrote what became a somewhat popular children’s book. It was called Beautiful Junk, A Story of the Watts Towers. That book, without my knowing it, was to become both Bible and Blueprint for my life. It was about a black boy who travels from breaking things in anger to finding beauty and creating things from what is broken. But to move from anger to love, he has to climb the fence that holds him in school.
That story brought me into classrooms as an author, and later as a writer-in-residence. Around that time I co-founded the Community Design Studio at the Southern California Institute of Architecture. The premise was that design is an inside job inspired by being on-site or in-residence, not by sitting in a cubicle at a drafting table. I also taught the psychology of design, which involved thinking about how intuition joins cognition to bring forth the best possible form and function, aesthetics and utility. It was the book, Beautiful Junk, that led a Foundation to provide seed money to bring other artists into an inner-city Los Angeles School. Together we created a design studio to work on curriculum. This was known as The Artist-in-Residence Reading Project.
From the Artist-in-Residence Reading Project, I learned that children are unbelievably beautiful, and pure when they are in their early grades, but by grade four, in the inner city, we begin to lose far too many children. I learned that to become mentally healthy, a person has to discover their voice and choice and to connect these to the symbol-making process. I learned that we are social animals, and that our didactic, shallowly academic curriculum stifles everyone’s social and expressive needs. I learned that the artists were able to light beautiful fires in classrooms and the teachers, with all good intentions, too often were persistent in stifling those fires. Finally, I learned that our team of artists was profoundly inspired by the children, and by the more open teachers. The artists got as much from being in the project as the children. That was an effect I had not anticipated.
A Humanistic Approach to Teaching Science.
I am inspired by the arts, science, and emerging technology. Science is the art and discipline of observing, caring, wondering, and asking questions in order to explore and discover. Science looks to go more deeply into the design of the creation. Science asks how the world works, how people function. What is most important within a given context.
Science is also the source of so much that we value in our culture. Science has made engineering at current levels possible. That engineering is now driving science, which in turn is improving our potential to avoid and treat diseases, process and communicate information, and discover the nature of the society and the universe — pushing our knowledge to ever more subtle levels that stimulate more questions, more inquiry and reflection, more measurement-based observation, and fertile uncertainty.
At heart, science is curiosity stimulated by observation, hypotheses joined to experimentation. It is wonderfully rational and incredibly intuitive and productive. The best in our society is a product of science joined to technology. And, of course, both science and technology are profoundly influenced by mathematical insights.
But science without aesthetics, without idiosyncratic inquiry, without personal symbol making, which art is largely about, risks becoming sterile. Where scientific inquiry is motivated by objective questions, objective observations, usually about the outer world, artistic inquiry is more motivated by a quest for self- or at least expressive knowledge. Artists experiment in self-expression, in finding their unique voice, in gaining insight into the unfathomable depths of being in this place with these people and with nature.
Both the Arts and Sciences are motivated by curiosity and inspired observation and pursued through focus and discipline, but they explore different realms. In the end, the arts document our struggle to achieve our sanity and humanity. The sciences pursue more objective knowledge of the creation.
That said, science can turn the light of its tools onto patterns in human intelligence, intuition, and so on. And the arts can provide an intuitive view of the horizons where objective knowledge dissolves into subjective awe and wonder.
STEM (Science, Technology, Engineering, and Math) provides the methods and tools to bring public education into the 21st Century in the very best sense. Personalised Learning can now realise its potential if we apply the sciences of management, learning, personality development, and diversity to how we design, diversify, and then publish our curriculum. We should be equally interested in teacher diversity and personalised staff development. This is a whole new field of Research, Development and Design.
Our current technology provides an unbelievable curriculum design environment, whether we’re designing for on or off-screen learning, and it has inherent within it entirely new research potentials that span all the factors that both inspire and limit our learning.
Finding the Right Balance What is the relationship between the Arts and STEM as relates to education? The Arts are a somewhat different form of inquiry and their expression is tuned to communication. What the arts offer to curriculum design is sensitivity to aesthetics, and aesthetics are the velcro that helps ideas to stick. So, when I think about forming a curriculum design team, I want to see lots of fine scientists and researchers, joined by artists, working together with software engineers.
I’m much less interested in the “application of STEM content” than in figuring out how schools begin to function as STEM studios, workshops, or laboratories. The classrooms are where the students and teachers are. This is where the design action is.
The best learning is immersive, not didactic. Content teaching tends to be dreadfully and redundantly didactic, though gifted presenters provide a wonderful exception as do productions by great media artists.
Being part of a research team, doing an R&D project with scientists, and designing curriculum by working with designers, that is an immersive and creative experience. STEM is to be lived, it’s an approach to life and a new kind of culture for our schools. STEM is not just to be learned about, it’s to be lived.
Having said that, we should do a much better job providing a curriculum that tells the amazing story of the history of science and technology in medicine, engineering, transportation, manufacturing, business, knowledge transfer (publishing), and so on. And we should tell the story of the main themes in cultural and human development.
The biographies of great physical and social scientists, mathematicians, engineers, and other people who have contributed to our culture should be well understood by students and their teachers. Our culture is lifted by their vision and dedication.
Thinking About Diversity in Our Teaching
Diversity is one of the great mysteries of the universe (uni-verse means one poem or song) because no two people are ever entirely alike nor are contexts ever repeated exactly. The Latin ideal on our national seal says “Unity Through Diversity.”
But diversity is not easily perceived because our egos tend to think everyone else is pretty much like us and we’ve been here before. We have trouble getting quiet enough to make room for people who are very different from ourselves and for new moments. It may well be that perceiving human diversity much more deeply is one of the frontier challenges we now face. Certainly, educating for diversity is a frontier challenge for school reform.
And here is where joining STEM with the Arts associated (STEAM) with great curriculum design is so important. Technology now makes it possible for us to do amazing research about diversity as it relates to learning, motivation, and our social and other drives. If we are serious about achieving personalised learning, we need to build that personalised learning on research into diversity. Prior to computer science, there was no way to operationalize this kind of research and then design and publish curriculum in response to the continuously evolving results.
However, I’m afraid that STEM in our schools has fallen far short of its potential because we have seen it as a subject, or set of subjects to teach, while failing to apply STEM to our teaching and learning, curriculum design, and assessment processes.
Ironically, while loudly declaring that education should be research-based, we continue to ignore the learning, management, and human development research while turning STEAM and SEL into subjects taught from textbooks and uninspired lectures in preparation for tests that serve a stupefying accountability.
When will we rise above the entropy of current practices and begin to learn and celebrate the benefits of inventing our knowledge culture?
""",
                        Topics = appDbContext.Topics.Where(t => t.Id == 25).ToList()
                    },
                    new Post
                    {
                        AuthorId = user.Id,
                        Title = "Self-Knowledge as a 21st century skill",
                        ImageUri = "http://localhost:5010/images/2.webp",
                        Description = """
In the quest to prepare students for a future filled with jobs that don't yet exist, we often emphasize critical thinking, creativity, and collaboration—skills that are vital for success in a rapidly evolving world. However, these skills are not independent of one another. They all rest on a foundational hub: self-knowledge. The question, then, is how can we design a curriculum that helps students develop a deep sense of self-awareness, which is the starting point for cultivating critical thinking, creativity, and collaboration?
The growing focus on Social Emotional Intelligence (SEI) represents a significant first step. SEI looks beyond the traditional academic focus on civics and good citizenship, helping students explore their own ever-evolving nature. This kind of learning has deep roots in humanistic traditions designed to foster self-knowledge—an awareness of one’s thoughts, emotions, motivations, and social interactions. But how can we fully realize this potential in our classrooms?
To evolve a curriculum for developing self-knowledge, we need to ask: What principles, concepts, and practices can help students cultivate self-awareness?
One such practice is Mindfulness, a growing entry point for exploring introspection and self-awareness. Mindfulness, often seen as the first step in meditation, has roots in spiritual traditions, martial arts, dance, and sports. Meditation, in essence, means to sit still, focus, and pay attention to what’s happening within. The goal is not to react, but simply to observe. Students are guided to notice their bodily sensations, thoughts, feelings, and intuitions, without judgment or immediate response. In doing so, they cultivate a quiet, reflective capacity. This allows them to step back from reactivity and become more responsive to the world around them.
Through mindfulness, students learn to observe without judgment, a key to self-awareness. When we observe, we notice that we often judge ourselves and others. The practice of mindfulness teaches us to notice judgments without becoming attached to them. For example, if a student judges their thoughts, mindfulness encourages them to return their focus to their breath, allowing the judgment to pass by without reacting. This fosters a habit of non-judgment and helps prevent the split between "superiority" and "inferiority" that judgments often create. By recognizing this internal tension, students can develop a more balanced, peaceful sense of self.
As students develop this reflective capacity, they realize that self-awareness is essential in social relationships. Healthy, mutual, and truthful relationships—the foundation of collaboration and problem-solving—require a sense of balance between one’s own needs and the needs of others. Democracy, at its core, teaches this balance, asking us to honor both our individuality and our shared humanity. This becomes an essential principle of self-knowledge—how we interact with others, respect diversity, and create a just, equitable world.
Historically, the psyche has evolved from authoritarian structures (families, tribes, monarchies) into more democratic ideals, where power is shared and understanding is prioritized. True democracy, both as a political structure and as a personal practice, requires mutual respect, empathy, and continual self-awareness. For the individual, it means relinquishing the need for control and embracing a cooperative, egalitarian approach to life. For the society, it means fostering deep collaboration and problem-solving, recognizing that none of us is as smart as all of us.
In education, this principle should underpin the creation of learning communities—spaces where students, teachers, and all members of the school community are co-creators in the learning process. A key aspect of this is helping students understand and navigate polarities within their own psyches. These inner conflicts—such as the tension between independence and dependence, love and hate, trust and mistrust—are part of the human experience. The arts, dreams, and personal reflections often express this inner struggle. Learning to navigate and integrate these polarities is central to personal growth and self-knowledge.
The more students can identify and integrate these polarities within themselves, the more they develop a balanced personality. Self-knowledge rooted in the integration of opposites creates a generous, peaceful individual who can function harmoniously within a community. This requires an openness to both disintegration and re-integration—the willingness to experience breakdowns in order to create new forms of understanding and wisdom.
This brings us to the concept of transcendence, the capacity to rise above conflict and see both sides of a polarity. Transcendence is what allows us to integrate opposites, bringing a new, more expansive perspective. It’s expressed through humor, art, and some of our most insightful dreams. Transcendence is about synthesizing the conflicting aspects of our personalities, allowing us to create a more integrated and peaceful self.
In the context of education, transcendence is about helping students not only understand subject matter but also understand themselves. In today’s efforts to reform education, we see a shift from purely academic curricula to more personalized learning approaches that emphasize self-reflection and growth. Portfolio assessments, for example, allow students to track their personal development—not just their academic progress—offering a more holistic view of who they are as learners and individuals.
To facilitate this deeper self-awareness, we can integrate practices that encourage introspection and symbol-making, including in the arts. For instance, art classes could go beyond the technical aspects of creating visual works and delve into the emotional and symbolic meanings behind the work. By examining the tensions and polarities expressed in art and personal symbols (such as dreams), students can better understand their inner conflicts and work towards integration.
Topics like wishful thinking, defensive thinking, group identification, manic and depressive moods, and symbolism in the arts and dreams can also deepen students’ self-awareness. Helping students explore their relationships with peers, and the symbols that influence their desires, would empower them to understand how external influences shape their internal worlds.
Ultimately, the maturation of each individual requires a healthy interest in who we are as evolving, complex, and interconnected people. If we can help students cultivate this self-awareness, they will be able to offer the same care, empathy, and understanding to others—transforming their relationships with peers, teachers, siblings, and parents.
""",
                        Topics = appDbContext.Topics.Where(t => t.Id == 25).ToList()
                    },
                    new Post
                    {
                        AuthorId = user.Id,
                        Title = "Unifying our vision for evolving education",
                        ImageUri = "http://localhost:5010/images/2.webp",
                        Description = """
Computer science is the spine that has created a continually improving R&D and distribution system in nearly every major endeavor. Examples include science, engineering, social science, medicine, retail, aviation, and entertainment. It has given us systems, tools, and techniques to enable us to design, organize, adapt, and share knowledge and techniques. That said, the largest knowledge industry and the one that touches the most people and is most important for the quality of people's lives and our future education is not supported by a systematic, scientific R&D agenda. To successfully reimagine learning, we need to optimize computer science to re-engineer and deepen our thinking about human nature, learning, design, and social interactions. By applying best practices from SEL (Social Emotional Learning) and STEAM (Science, Technology, Engineering, Arts, Mathematics), we can evolve toward personalizing learning for all students by designing robust and engaging curriculum that aligns with the stages, diversities, motivations, and idiosyncrasies unique people’s development. These efforts will immerse students in a SEL and STEAM culture where they co-design their learning experiences. This will give students a practical initiation into our 21st century knowledge economy. Let’s consider the realms of computer science and engineering that will be foundational for building fully reimagined learning processes. These include R&D in social interactions, research into all dimensions of diversity among students and teachers, knowledge engineering, and new design paradigms.
The many roles of computer science and engineering in 21st century learning systems
Social Engineering helps us to use technology wedded to our social, cultural, psychological, and community-organizing sciences to create smart, compatible learning groups. These groups allow the intelligent connection of students and teachers with the appropriate peers, tutors, cross-age role models, and mentors. As we move towards personalized learning, we are challenged to learn about the unique dimensions of each person in our community. We should ask: how can we use computer science to learn about the range and depth of sensory, social, emotional, intuitive, and cognitive traits and values that will enable us to support and enrich the lives of students, teachers, administrators, parents, and others. Think of the possibilities if we were to adapt and extend some of the computer science used by online matchmaking services and product marketing. Our social networks for student support would go far beyond dating and volunteer matching services -- we would explore personality traits, goals, interests, cultural considerations, communication, learning styles, and other dimensions we've yet to consider. Such social networks would be dynamic and self-refining, as they would continually gain machine and human intelligence through use, users, and changing purposes. We can profoundly improve learning if we put the entire local, face-to-face, and virtual community at our students' and teachers' fingertips. This means that every student, whether their passion is rocks or rockets, music or mountaineering, math or mashed potatoes, can find congenial peers and experts, friends and collaborators, facilitators and a supportive audience to support their interests. Given our deep drives to work and learn together to solve authentic personal, social, cultural, and environmental problems, having access to focused social networks can propel learning into the realms of service that will help establish purpose, identity, and compassion. The implications of building and improving social networks for students, teachers, administrators, and curriculum designers can't be overestimated.
Research Engineering will help us to better understand and respond to the diversity among students, teachers, parents, and community members. This research will build from a rich variety of non-invasive measures correlated with observations and reflections. The science of building systems to improve data will help us design and align personalized learning experiences. Essential factors to understanding the wide range of variables that influence each student's learning include sensory capacities, concentration, working memory, motivations, background knowledge, openness, perseverance, interests and so much more. Recognizing that a learner's sense of purpose affects cognition and imagination and is built on a social-emotional footing means we need to focus research on identifying the social-emotional experiences that build solid foundations within each student and teacher. One under explored area is a smart learner portfolio that will also include social networking with family, friends, and mentors. Social and machine feedback features and appreciations will be balanced. Simply having information about learners' writing in terms of the richness of vocabulary, syntax, and other machine measurable variables will indicate a great deal about student engagement and cognitive development. The implications of students, teachers, mentors and curriculum designers understanding key dimensions of diversity are profound beyond our current imagining.
Knowledge Engineering grows directly from Research Engineering, and extends into the design, organization, and sharing of learning experiences. There are many ways to apply technology to support the design, organization and continuous improvement of learning experiences. The content of a learning experience, be it a lecture, video, field trip, poem, story, simulation, or service project can and should have many designs that will improve access across different ranges of ability, interest, and talent. For example, the same story can be written at multiple reading levels and in many languages. This represents an important step toward Universal Design for Learning, a framework to improve and optimize teaching and learning for everyone based on insights into diversity. We should note that not all curriculum created by technology supported design should be online, far from it! Small group discussions, texts, board games, simulations, worksheets, community service and environmental projects should be designed and supported by technology, yet many of the activities would be done away from the computer screen, and many will involve using mobile devices in the field. Knowledge Engineering provides a framework for refining and applying our understanding of the scope, sequence, and scaffolds to optimize learning. It helps manage customization, version control, and dissemination. It also uses the data about the student like reading level, interests, and favorite friends to provide students with a good set of customized choices about what, how, when, where, and with whom each student would like to learn.
Design Engineering is a subset of Knowledge Engineering. It provides educator-designers a unique set of design, presentation, interaction, assessment, and feedback tools to help design teams shape their designs to fit the wide-ranging sensory, affective, cognitive, intuitive, and aesthetic needs of students, teachers and communities. The first principles of design are: 1) design with and in the presence of the people and community you serve; and 2) create something your clients' love; and 3) be able to adapt and modify based on experience. To achieve these, it's essential that teachers and students be co-designers. Designers need to watch and listen. They need to ask for help and feedback. Great design is inspired by love for the users and love for the subject or skill to be mastered. When we connect social and knowledge engineering to research and design, we can make our classrooms into smart design studios in which in-residence, cross-discipline teams participate and become co-creators with students and teachers. A digital latticework that is created and maintained by in-residence computer scientists and engineers who continually evolve the system to serve the learning community's needs is fundamental to support this design work.
""",
                        Topics = appDbContext.Topics.Where(t => t.Id == 25).ToList()
                    },
                    new Post
                    {
                        AuthorId = user.Id,
                        Title = "We have failed personalized learning",
                        ImageUri = "http://localhost:5010/images/2.webp",
                        Description = """
Rightly hailed as alchemical gold for public education, Personalized Learning is failing to realize its potential. This should worry us because the fault is not in the principles of Personalized Learning, but in our failure to grasp its meaning and therefore to do the groundwork to lay a proper foundation. To understand this, let’s define Personalized Learning as the organization, presentation, and scaffolding of learning experiences that meet the deepest needs of students. This means that the curriculum addresses learner concerns and supports agency, and autonomy; it empowers students in making meaning and addressing problems while discovering knowledge, skills, maturity, and satisfying relationships. This is a tall order in a huge system that history has shaped around didactically teaching separate academic subjects from textbooks, worksheets, and lectures, at relatively shallow levels to prepare for tests. So Personalized Learning challenges us to think beyond testable subject knowledge and technical skills, though both these areas can be addressed in a Personalized Learning focus. How can we move beyond a narrowly prescriptive approach to academic and technical knowledge and skills? Personalized Learning starts with the individual learner and therefore it asks us to understand human diversities. The plural is used here because unlike the diversity of fingerprints or even blood types, in the realm of what people want to learn, how they will learn, at what rate, and for what reasons we find people are much more diverse than we can fully grasp. In fact, the combination of character and learner traits appears to be endless! Here’s an example: two students are motivated to learn to read but are struggling. Are their difficulties at a visual or auditory perceptual or short or long-term memory level? Or is there a deficit in linguistic memory or background knowledge? Or should we be looking for cultural or personality factors or a combination of all of these? In this example what appears as the same learning problem may occur for entirely different reasons. If we think in biological terms, we might say that at the phenotype level the problem is the same, the presenting symptoms are the same, slow development of reading skills, but at the genotype level there are a number of possible differences. A simple example: At the genotype level one student may have poor visual or left to right acuity while the other may not hear the differences in similar letter sounds. At a more complex level, the difference may relate to a failure to develop background knowledge or narrative skills needed for comprehension. This takes us to the crux of our failure to make good progress in Personalized Learning. While Personalized Learning says it wishes to address the unique individual differences and create learner success, in truth we have stayed at the phenotype, or surface level; we have not begun to look at learner genotypes, or the combination of learner-traits that are predictive of what, 
how, when, with whom, and for how long a learning experience should be undertaken. Even at the phenotype level we’ve failed to respond to obvious learner needs, like the need for immersive social support through interactions with others. Why? Because the tools for diagnosing learning at the genotype level have just arrived via digital systems and we have not fully grasped how the vast realms of developmental psychology can be used to address learner traits on multiple continuums that range from perceptual acuity, to personality needs, affective, social, and intellectual interests and concerns. Being able to see into people’s inner worlds to explore diversity is a vast new realm that can be compared with using the digitally enabled Hubble and Webb telescopes to peer into space. Similar hardware, whether tracking the birth of stars or a student’s visual or auditory acuity, oral or written narrative skills, or moods and enthusiasms, has delivered us to the cusp of developing a personalized pedagogy to support personalized learning. However, to do this we need to think about what data we want to gather from psychometrics and observation in new and coordinated ways. We must see beyond categorizing in ways that risk stigmatizing or stagnating learning to ways that view people along dynamic developmental continuums where learning experiences are meaningful and are built on solid scaffolds that promote growth. To date, STEM has been our focus along with Personalized Learning, but the Science part of STEM has been limited to our hard science, and our human sciences have been ignored. We have focused on building a knowledge economy based on hard sciences, technology, math, and engineering while ignoring our sciences of human development, learning, collaboration, culture and meaningful problem solving. This is a costly oversight because our learning and social sciences have so much to tell us about what matters most to whom and these sciences can guide our curriculum design processes in ways that digital systems are now able to support. To build the knowledge economy, we must broaden our definition of STEM (STEAM), not by adding more letters, like SEL, but by defining science much more broadly. We must see that we want to build a knowledge culture, which means building a learning culture within our schools. Within our schools is a key concept. Currently most of our curriculum, including our STEM curriculum, comes from people working in offices. Likewise, the engineers creating educational software are seldom in schools. Currently marketing directs curriculum design by analyzing government policy and buyer focus groups. Teachers and learner sensitivities get little air. If we wish to achieve Personalized Learning, we need to link our research to a new design philosophy that understands how digital systems can support an in-residence project-based design process that includes students, teachers and a diversity of curriculum artists in-residence. This system will support continuous improvement and diversification of learning resources. The Internet was first invented to enable scientists to see what was happening in particle accelerators so they could observe and collaborate in building understanding about subatomic events. What will happen now if we build a knowledge development platform to help us observe and collaborate to build the most engaging Personalized Learning for students?
""",
                        Topics = appDbContext.Topics.Where(t => t.Id == 25).ToList()
                    },
                    new Post
                    {
                        AuthorId = user.Id,
                        Title = "The promise of AI to move to a more learner-centered curriculu",
                        ImageUri = "http://localhost:5010/images/2.webp",
                        Description = """
With the invention of the microcomputer, innovative educators envisioned a renaissance in instructional design. They imagined a future where lesson plans could be easily written, revised, and personalized to meet individual student needs, fostering continuous improvement in educational materials. Many believed this would lead to more dynamic and less expensive publishing, giving rise to a new era in education. However, this vision largely failed to materialize. Instead of driving innovation in teaching methods and materials, technology in education became more closely aligned with assessment and high-stakes testing, which took precedence over creativity and refining instructional design. The arrival of the internet and collaborative authoring tools sparked renewed hope for another renaissance. Educators and technologists imagined a world where curriculum design could be customized (personalized), shared globally, and continuously updated to reflect the latest pedagogical insights. Again, this potential went largely unrealized. While there were pockets of innovation, the dominance of standardized curricula and a focus on testable outcomes limited the widespread adoption of more personalized and dynamic instructional methods. Today, artificial intelligence (AI) presents a third rebirth opportunity. The question now is whether we can leverage AI to shift away from a siloed academic curriculum and assessment process to one that is more personalized and aligned with what the science of learning tells us about human development. This approach would focus on holistic education, incorporating social-emotional learning (SEL), STEM, and the arts to foster well-rounded development, rather than limiting educational success to narrow measures of academic achievement.
Steps needed to realize the potential of AI to build a more learner-centered culture in schools: One of the most significant obstacles to creating a learner-centered curriculum is the prevailing emphasis on standardized testing. To move beyond this, we need to redefine educational success to encompass broader measures of student growth. AI can help by providing tools that assess a wider range of competencies, such as critical thinking, creativity, emotional intelligence, and collaboration. AI-driven analytics can offer real-time insights into these areas, enabling educators to design more holistic educational experiences tailored to each student's motivational and expressive needs. AI has the potential to transform instructional design by creating adaptive learning platforms that tailor educational content to individual learning styles, paces, and interests. Such platforms can use data on student performance to provide personalized recommendations, resources, and activities that align with each student’s strengths and weaknesses. For example, AI can offer customized learning paths that adjust in real-time based on a student's progress, providing immediate feedback and additional support where needed. AI tools can facilitate the integration of interdisciplinary learning, where students explore connections between subjects like math, science, the arts, and social studies. By identifying patterns and relationships across different fields, AI can help educators design curricula that break down the traditional silos of academic subjects. Additionally, AI can allow for customization at scale, enabling educators to draw from a vast pool of resources to create lesson plans that are specifically tailored to the diverse and evolving needs of their students. 
AI can support collaborative learning by providing platforms that encourage peer-to-peer and cross-age interaction and teamwork. It can also enable more creative learning experiences by offering tools for digital storytelling, interactive simulations, and virtual reality experiences. For example, AI can help design project-based learning activities that require students to work together to solve real-world problems, 
integrating multiple disciplines and fostering a sense of purpose and engagement.
""",
                        Topics = appDbContext.Topics.Where(t => t.Id == 25).ToList()
                    },
                    new Post
                    {
                        AuthorId = user.Id,
                        Title = "How AI can support developing a holistic spiral curriculum",
                        ImageUri = "http://localhost:5010/images/2.webp",
                        Description = """
We know the many ingredients that inspire engagement for learning: background knowledge, belonging, play, self-expression, wonder, beauty, empowerment, and imagination. Stories are among our oldest simulations to engage learning. It seems we are hardwired to create stories. Even as we sleep we dream in whole stories or in little vignettes. And we’re eager to share our good, bad, or weird stories as well as our night and day dreams! It seems that people are natural storytellers and receivers. Surely sharing a well written, age-appropriate story offers several kinds of imagination-magic. The active imagination stills the listener / reader into quiet, receptive, engagement. Fertile ground for wonder! The storyteller or reader gains rapport without being inventive or creative. The author has done that work for the facilitator. While experiencing a story, all share in a common, contemplative learning experience that calls to be explored through further inquiry. Nothing stimulates receptive listening better than asking students: Given the title, what do you imagine this story may be about, and then stopping at key points in the story to ask, what do we imagine may come next. For decades many curriculum designers have wondered how to create a Spiral Curriculum, one that would build from primary grades through graduation into a realm of knowledge deeply aligned with student developmental needs, cultural and societal requirements for belonging. Could we go from a siloed, academic curriculum with narrowly defined measurements to a more integrated and holistic curriculum where knowledge and skill development equated with empowerment, identity, and enthusiasm for learning. Prior to AI, the challenges of creating such a Spiral Curriculum overwhelmed our capacities so we settled for the academic / skill siloes at the price of teacher and student boredom and burnout. With AI we can change this. We can ask what are the key ideas that stimulate children to be interested, to wonder, feel belonging, safety trust and empowerment. 
One such key idea is the science story of the relationship between sunlight and plants (photosynthesis). Without the help of AI I would not have attempted to write a story that explores this relationship though I had successfully introduced the wonder-filled idea that E=mc2 to 3rd graders. Below is the prompt I used to get ChatGPT to produce a story presented as a dialogue between sunlight and a sunflower. Following the prompt is the lightly polished story built from what ChatGPT provided... I offer this to demonstrate the possibility of creating stories for a well scaffolded spiral curriculum that takes SEL into the realm of STEM or STEAM and circles back to inspire an ever growing identity born wonder that transcends knowing and provides the soil in which inquiry and trust might be nurtured that our human spirit might, through education, become at home in the world.
""",
                        Topics = appDbContext.Topics.Where(t => t.Id == 25).ToList()
                    },
                    new Post
                    {
                        AuthorId = user.Id,
                        Title = "Why Stories for Teaching: Insights for Educators ",
                        ImageUri = "http://localhost:5010/images/2.webp",
                        Description = """
Engagement of Multiple Brain Systems Stories tap into parts of the brain that process logic and emotion, creating an immersive learning experience. On the cognitive side, stories activate areas responsible for comprehension, reasoning, and memory. The left hemisphere, which handles language, breaks down the structure of a story—understanding syntax, meaning, and plot. On the emotional side, stories activate the limbic system, especially the amygdala, responsible for processing emotions causing story content to be memorable as students emotionally connect with characters and situations. Additionally, our brain's mirror neurons "feel" what characters feel. This is why well-told stories evoke joy, fear, or sadness, enriching the learning experience. Stories Enhance Memory Retention Stories follow a narrative structure—beginning, middle, and end—that aligns with how the brain organizes information. This structure helps students recall complex ideas more easily than when isolated facts are presented for memorization without a rich context. Additionally, the brain is wired to recognize patterns, and stories are full of them. Whether cause-and-effect 
relationships, moral lessons, or character development, the brain processes and stores these patterns, helping students apply them to real-life situations. Cognitive Development Through Stories Stories foster abstract thinking. By using metaphor, allegory, and symbolism, stories challenge students to think beyond the literal, enhancing their ability to think critically and flexibly. Fables, parables, poems, and religious texts, for instance, encourage students to interpret deeper meanings, broadening their cognitive skills. Moreover, stories often present characters facing problems. Students vicariously experience decision-making processes and the consequences that follow. This develops problem-solving abilities, moral reasoning—skills needed to navigate life’s complexities. Developing Emotional and Social Intelligence Stories can significantly boost empathy by showing the world from another person’s perspective. Understanding a character's emotions and motivations strengthens theory of mind, which helps students better navigate social relationships. Stories play a crucial role in cultural and moral education. Through biographies, fables, and religious texts, students learn societal values, ethics, and norms. These stories encode the wisdom of cultures passing up lessons that guide thinking, behavior, and social interactions. Neurochemical Rewards from Storytelling Neurochemical reactions make storytelling rewarding. When stories build suspense or moral dilemmas, the brain releases dopamine, a chemical associated with pleasure and reward. This creates positive feedback loops, making students more engaged and eager to learn. Stories emphasizing human connection or kindness release oxytocin, often referred to as the "bonding hormone." This neurochemical fosters feelings of trust and connection, making stories powerful tools for building community in the classroom. Evolutionary Role of Storytelling Storytelling evolved as a tool for survival. It allowed early people to pass down essential information—social rules, moral lessons, or warnings about dangers—without requiring risking firsthand experiences. This ability to learn indirectly through stories helps cultures transmit knowledge and wisdom efficiently. Making Sense of the Complexities of Life Finally, stories help students make sense of life's uncertainties and complexities. Whether it's mythology explaining natural phenomena or novels exploring human relationships, stories offer frameworks that help students understand concepts too abstract or mysterious for pure logic. In sum, stories speak the brain’s holistic language, blending logic, emotion, empathy, and imagination. By using stories, teachers can tap into this natural human affinity for narrative, making education meaningful and memorable!
""",
                        Topics = appDbContext.Topics.Where(t => t.Id == 25).ToList()
                    },
                    new Post
                    {
                        AuthorId = user.Id,
                        Title = "To teach reading teach numbers first ",
                        ImageUri = "http://localhost:5010/images/2.webp",
                        Description = """
The first step to reading is NOT memorizing the alphabet or letter sound relationships; it's about building an understanding of order and symbols. 
Before mastering the abstract world of letters, children should first grasp the concrete world of objects. Why? Because before a child can group 
and order letters to read, they must be able to group and order concrete objects. This fundamental skill develops as a child sorts toys by color 
or size, or lines up blocks from tallest to shortest. These activities create the knowledge that objects can be organized in specific sequences. 
This hands-on, tangible experience is the first step toward seeing and then understanding order. Consider that before a child can read, they must be able to arrange items in an ordered line, say "apple, pear, plum, lettuce, egg.". If you ask them to sequence the first letter of each item—a-p-p-l-e—you're asking them to make a massive cognitive leap. Sequencing objects precedes the abstract task of sequencing symbols. Numbers are the Bridge from concrete objects to the abstract symbols of letters because they are simpler symbols than letters in two ways: Fewer Symbols: There are only 9 basic numerals (1-9), compared to 26 letters. This smaller set is less intimidating and should be 33% easier for a child to learn. Direct Correspondence: Each numeral has a one-to-one correspondence with the quantity it represents. The number 1 always means "one of something," whether it's one apple or one person. The meaning is unchanging. This direct link makes numbers a reliable symbol system. In contrast, the meaning and sound of letters can change dramatically depending on their position and surrounding letters. The letter 'e' is a great example. In the word “example” ‘e’ has a sound at the beginning and is silent at the end. This ambiguity makes letters far more complex than numbers. Once a child reliably sees and understands that the symbol '5' stands for five of something and they can place a specific something in the first, third or fifth place they are ready to tackle the larger, more complex world of letters sequenced to make words. They can explore that a letter’s meaning is not fixed. While an egg is always an egg, the letter 'e' is a symbol with many assigned sounds—and those sounds change depending on the letter’s location. The journey from arranging objects to reading words should progress from concrete to abstract. Start with familiar items, move to the consistent world of numbers, and finally explore the many rules of letters. This builds a solid cognitive-perceptual foundation ensuring that children enjoy developing their understanding of the systematic way written language produces meaningful sounds.
""",
                        Topics = appDbContext.Topics.Where(t => t.Id == 25).ToList()
                    },
                    new Post
                    {
                        AuthorId = user.Id,
                        Title = "Teaching Kids About Bravery: 'I Am Not Brave' Children's Book",
                        ImageUri = "http://localhost:5010/images/2.webp",
                        Description = """
What does it really mean to be brave? Is your child afraid to try new things? Do they cave to peer pressure or think being brave means being 
reckless? "I Am Not Brave" by Jon Madian flips the script on courage, teaching children that real bravery sometimes means saying "no" and 
listening to your gut because fear can be your friend. This isn't your typical "face your fears" children's book. Instead, it offers a refreshing 
take that will have both you and your child rethinking the heart of courage. Set in a quiet forest and brought to life through gentle dialogue, 
this story follows a tender conversation between Rabbit and Donkey as they unpack fear, courage, and how to make wise choices. Perfect for 
classrooms, libraries, learning centers, or home, this book opens the door to important social-emotional learning (SEL) lessons that last a 
lifetime. The Heart Bright Learning educational resources evolved from 7 years of generous Research and Development funding from the Los Angeles 
Unified School District, the U.S. Department of Education, and several foundations. Our literature-based approach, which inspires imagination, 
exploration, and self-expression through structured practice, was studied by several outside evaluators. In all cases, these evaluations 
demonstrated significant growth in listening, speaking, writing, and reading skills among both challenged and gifted students from a diverse 
range of cultural and language backgrounds. In I Am Not Brave, Rabbit shares a quiet moment with her friend Donkey and admits something she’s 
never said out loud: “I’m always afraid.” What follows is a gentle, thoughtful conversation between two friends. Together, they explore what it 
means to feel scared, how to stay safe, and how making good choices—even when others don’t understand—can be the bravest thing of all. Rather than 
portraying bravery as bold or reckless, the story shows that true courage often means listening to your fears, thinking things through, and 
staying true to yourself. It’s a message that resonates deeply with children and gives adults a natural, meaningful way to talk about fear, peer
 pressure, and emotional strength. This fable can be read as a standalone or as part of a growing series of books supporting social-emotional 
 development through storytelling. Many children — and adults — equate bravery with recklessness or daring acts. But I Am Not Brave gently shifts 
 that narrative. Through Donkey’s kind, listening presence, children learn: That feeling afraid is natural, and even wise That peer pressure 
 doesn’t define courage That bravery can mean saying no, walking away, or choosing safety over risk That being honest about your feelings is a 
 brave act in itself This message is especially valuable in classroom settings, where children often compare themselves to others. The story 
 invites discussion about self-awareness, empathy, and standing firm in what feels right — even when others don’t understand. This story covers 
 thoughtful questions for educators and parents to stop and discuss with children during the story to explore important social-emotional concepts: 
Is it hard to tell anyone about things you are afraid of? What do we think it means to be brave? Have you had a conversation with a friend that 
has helped you with a problem? These questions create space for emotional honesty, peer learning, and self-reflection. They build empathy—a key 
ingredient in every healthy classroom or home. Discussion prompts are included throughout the Stop&Talk version of the book to deepen understanding 
and encourage meaningful conversations about courage, friendship, and emotional intelligence. Teachers and caregivers can use I Am Not Brave in: 
SEL-themed story circles Reading comprehension lessons focused on character dialogue Creative journaling or drawing activities (e.g., “Draw what 
bravery looks like to you”) Roleplay scenarios on peer pressure, empathy, or fear Additional extension activities can be found here At home, this 
book can open meaningful conversations during bedtime or help children prepare for new or scary situations — like trying something new or handling 
a conflict.
""",
                        Topics = appDbContext.Topics.Where(t => t.Id == 25).ToList()
                    },
                    new Post
                    {
                        AuthorId = user.Id,
                        Title = "The Best To-Do List App",
                        ImageUri = "http://localhost:5010/images/3.webp",
                        Description = """
Mastering your to-do list can seem like a Sisyphean task, but a good to-do list app should help
you regain control over your routines and make it possible to keep chaos at bay.

Our to-do list app picks, Todoist, TickTick, and the Apple-exclusive Things 3, are a breeze to use,
have thoughtful designs, and feature flexible organization schemes, so you can conveniently hop in,
address your obligations, and enter new tasks—then get right back to the doing.
""",
                        Topics = appDbContext.Topics.Where(t => t.Id == 25).ToList()
                    },
                    new Post
                    {
                        AuthorId = user.Id,
                        Title = "BUILDING AUDITORY AND VISUAL REGISTERS WITH TORTOISE TEACHES RABBIT TO READ",
                        ImageUri = "http://localhost:5010/images/3.webp",
                        Description = """
The discovery pedagogy behind TORTOISE TEACHES RABBIT TO READ (TTRR) builds on curiosity, observation, counting and the resulting focus that 
naturally occurs. This is in stark contrast to redundant cycles of drill-and-practice. As children focus to discover auditory and visual 
patterns and to analyze those patterns, they apply very basic math (set theory and ordinal concepts) to words. In this way, children naturally 
develop their visual and auditory registers. We hypothesize that focusing is key to developing these registers or special arrays and they are 
the fundamental neurological basis for seeing numbers and letters in a string. To read, the brain must develop several remarkable capacities. 
First comes the ability to focus on hearing words as one or more sounds. Next, we must see geometric shapes, discriminate among shapes or 
individual letters, and then master grouping of letters into a sequence to build a word. To accomplish this the ear-eye-brain system must 
build what we might think of as auditory and visual registers or arrays for pattern displays. Children learning math need to build up a right 
to left register in the brain that enables placement of numerals in the ones, tens, hundreds, or thousands columns. Likewise in learning to read, 
we need to develop a left to right register, or set of columns, for the first, second, third, and all following letters. Of course, syllables help
 us to group letters in multi-syllable words in much the same way that the tens', hundreds', thousands', groupings enable us to easily organize and
  see the elements of numbers. The question we previously failed to ask in teaching reading is how to most efficiently and explicitly help
   youngsters to build these registers so they can most effectively perceive auditory and visual textual information. In TTRR we present a very 
   attractive and motivating approach to focus children, so they pay attention to text and how it works; thus, they naturally build their
    registers for words and then for written phrases and sentences. First, the children see Rabbit discover that his name, Rabbit, has more than
     one sound and that the many sounds in his name can be represented by geometric shapes, letters, drawn on the ground in a linear way. Rabbit
      begins by seeing these as footprints that sounds leave behind. Following the story, the child sees the importance of the order of letters
       when Tortoise helps Rabbit to study Rabbit's name, at first represented by just three letters R-B-T. Later, as more letters are added, our 
character discovers more sounds--R-A-BB-I-T. In this auditory approach to written text, we apply the STEM pedagogy that supports invented spelling.
""",
                        Topics = appDbContext.Topics.Where(t => t.Id == 25).ToList()
                    },
                    new Post
                    {
                        AuthorId = user.Id,
                        Title = "Balancing spirit and academic traditions",
                        ImageUri = "http://localhost:5010/images/3.webp",
                        Description = """
For good reasons, we believe that church-related beliefs should not influence public education, unless the beliefs are 
part of a curriculum that involves the history or comparison of religions. With that in mind, and with our emphasis on 
educating the individual, it seems that concepts involving spirit are off limits in our schools. Similarly, in India, the 
cradle of so many spiritual traditions, there is a national interest in Values Education and an abhorrence of such a curriculum 
coming from any of India's many spiritual traditions. Colonial influences have left India as secular about education as the United States. 
This becomes important when we grasp that Values Education is a secular frame for millennia of spiritual traditions. 
The world's first cultures see the environmental and cultural genocide by colonial and post-colonial “commercial”, evangelical, 
and industrial countries as symptomatic of a lack of spirit, a disharmony with the beauty, balance and consciousness of the earth. 
Reasonably, they suggest that our environmental and related crises are caused by human disharmony, or a lack of spirit, sensitivity, 
or we might say sympathetic, intuitive, and aesthetic resonance with the creation. Yet, ironically, our schools are all about spirit rallies. 
In these exercises of team spirit we use sports to light the torch of a collective belief and passion similar to nationalism during wartime. 
Thus, we might hypothesize that inspiring spirit, a collective purpose that inspires deep enthusiasm, in the service of sports is a healthy 
secular activity. But inspiring spirit, as a collective, unifying concept for the care of one another and the earth is not. It's time we took 
another look at the relationship between secular disciplines and spiritual teachings. While I'd argue for keeping specific religious teachings 
out of our schools, in the name of psychology, anthropology, comparative religions, teamwork, and shared purpose, I'd argue that to be fully 
human, which may be one good definition of being fully educated, people need to understand the towering spiritual traditions that teach to the 
heart of our humanity. Teaching such a balanced curriculum should NOT preach beliefs that come from any specific spiritual tradition. Indeed, 
by comparing these traditions, we can learn the common themes and divergences. We can also learn that many great people who were not within 
any religious tradition, held values in common with believers. Good and great people come clothed in widely varying spiritual and secular robes. 
Education should not be about maintaining ignorance about a core element in our nature--our joy in joining fully in events that quicken our sense 
of spirit. If people are not educated to the dangerous excesses of belief and spirit, as manipulated by authoritarians, then a secular education 
may leave people lonely and vulnerable rather than united and empowered to sympathetically serve our collective values, which are so similar 
across all spiritual and humanistic traditions.
""",
                        Topics = appDbContext.Topics.Where(t => t.Id == 25).ToList()
                    },
                    new Post
                    {
                        AuthorId = user.Id,
                        Title = "Apple Explores A.I. Deals With News Publishers",
                        ImageUri = "http://localhost:5010/images/3.webp",
                        Description = """
The company has discussed multiyear deals worth at least $50 million to train
its generative A.I. systems on publishers’ news articles.

Apple has opened negotiations in recent weeks with major news and publishing organizations,
seeking permission to use their material in the company’s development of generative artificial
intelligence systems, according to four people familiar with the discussions.

The technology giant has floated multiyear deals worth at least $50 million to license the archives
of news articles, said the people with knowledge of talks, who spoke on the condition of anonymity
to discuss sensitive negotiations. The news organizations contacted by Apple include Condé Nast,
publisher of Vogue and The New Yorker; NBC News; and IAC, which owns People,
The Daily Beast and Better Homes and Gardens.
""",
                        Topics = appDbContext.Topics.Where(t => t.Id == 25).ToList()
                    }
                );
                appDbContext.SaveChanges();
            }
            app.UseCors(options =>
                options.WithOrigins("http://localhost:5173")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
            );
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
        }

        var meiliSearchService = app.Services.GetRequiredService<MeiliSearchService>();

        try
        {
            await meiliSearchService.TestConnection();

        }
        catch (Exception ex)
        {
            Console.WriteLine($"MeiliSearch connection failed: {ex.Message}");
            // Optionally, you can exit the application if the connection fails
            Environment.Exit(1);  // Exit with an error code (optional)
        }

        app.Run();
    }
}