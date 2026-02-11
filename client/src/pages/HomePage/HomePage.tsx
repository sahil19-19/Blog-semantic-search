import { useNavigate } from 'react-router-dom'
import s from './HomePage.module.scss'
import logo from '../../assets/img/logo.png';
import search_banner from '../../assets/img/banner.png';
import semantic from '../../assets/img/semantic.png';
import OldHomePage from '../OldHomePage/OldHomePage';

const HomePage = () => {
  const navigate = useNavigate()

  return (
    <>
      {/* HEADER */}
      <header className={s.header}>
        <div className={s.container}>
          <div className={s.nav}>
            <div className={s.brand}>
              <img
                src={logo}
                alt="Levelworks Logo"
                className={s.logo}
                onError={(e) => {
                  (e.target as HTMLImageElement).style.display = 'none'
                }}
              />
            </div>

            <button
              className={`${s.btn} ${s.btnPrimary}`}
              onClick={() => navigate('#')}
            >
              Login
            </button>
          </div>
        </div>
      </header>

      <main>
        {/* HERO */}
        <section className={s.heroSection}>
          <div className={s.container}>
            <div className={s.hero}>
              <p className={s.eyebrow}>IN FOCUS</p>
              <h1>Semantic Search</h1>

              <p className={s.heroText}>
                Help users find what matters faster with meaning-aware search
                that understands intent, context, and natural language.
              </p>

              <div className={s.heroActions}>
                <button
                  className={`${s.btn} ${s.btnPrimary}`}
                  onClick={() => navigate('/demos/semantic-search')}
                >
                  Try it Now
                </button>
              </div>

              <div className={s.heroBanner}>
                <img
                  src={search_banner}
                  alt="Semantic Search Banner"
                />
              </div>
            </div>
          </div>
        </section>

        {/* QUICK WINS */}
        <section className={s.section} id='quick-wins'>
          <div className={s.container}>
            <h2 className={s.sectionTitle}>Other Quick Wins</h2>

            <div className={s.grid}>
              <article className={s.card} onClick={() => navigate('#')} style={{ cursor: 'pointer' }}>
                <div className={s.cardMedia}>
                  <img src={semantic} alt="Semantic Search" />
                </div>
                <div className={s.cardBody}>
                  <p className={s.cardFocus}>IN FOCUS</p>
                  <h3>Semantic Search</h3>
                  <p>
                    Modernize your .NET app with AI-powered features like natural language inputs, automation, and embedded assistance — without a rewrite.
                  </p>
                </div>
              </article>

              <article className={s.card} onClick={() => navigate('#')} style={{ cursor: 'pointer' }}>
                <div className={s.cardMedia}>
                  <img
                    src="https://images.unsplash.com/photo-1553877522-43269d4ea984"
                    alt="Observability"
                  />
                </div>
                <div className={s.cardBody}>
                  <h3>Observability</h3>
                  <p>
                    Catch issues before users do with full-stack visibility across logs, metrics, and traces — all in one place. 
                  </p>
                </div>
              </article>

              <article className={s.card} onClick={() => navigate('#')} style={{ cursor: 'pointer' }}>
                <div className={s.cardMedia}>
                  <img
                    src="https://images.unsplash.com/photo-1677442136019-21780ecad995"
                    alt="AI Enablement"
                  />
                </div>
                <div className={s.cardBody}>
                  <h3>AI Enablement</h3>
                  <p>
                    Add practical AI capabilities to your existing systems — copilots, smart workflows, and automation — built around your current stack. 
                  </p>
                </div>
              </article>

              <article className={s.card} onClick={() => navigate('#')} style={{ cursor: 'pointer' }}>
                <div className={s.cardMedia}>
                  <img
                    src="https://images.unsplash.com/photo-1551288049-bebda4e38f71"
                    alt="Admin Console"
                  />
                </div>
                <div className={s.cardBody}>
                  <h3>Admin Console</h3>
                  <p>
                    Replace manual, error-prone operations with a secure admin console for access control, approvals, and audit-ready workflows.
                  </p>
                </div>
              </article>
            </div>

            <div className={s.divider} />
          </div>
        </section>
      </main>

      <footer className={s.footer}>
        <div className={s.container}>© levelworks.co</div>
      </footer>
    </>
  )
}

export default HomePage
