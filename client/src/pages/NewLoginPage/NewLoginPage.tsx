import s from './NewLoginPage.module.scss';
import logo from '../../assets/img/logo.png';
import platHomePageImage from '../../assets/img/plat-homepage.png';
import { useNavigate } from 'react-router-dom';

const NewLoginPage = () => {
    const navigate = useNavigate();
    return (
      <div className={s.container}>
        {/* Left Side: Image/Branding */}
        <section
            className={s.imageSection}
            role="img"
            aria-label="Application branding"
            style={{
              backgroundImage: `url(${platHomePageImage})`,
            }}
          >
        </section>
  
        {/* Right Side: Sign In Form */}
        <section className={s.formSection}>
          <div className={s.formContainer}>
            <header className={s.formHeader}>
              <a
                className={s.brand}
                href="https://www.levelworks.co/"
                target="_blank"
                rel="noopener noreferrer"
              >
                <img
                  src={logo}
                  alt="Application Logo"
                  className={s.logoImg}
                  onError={(e) => {
                    (e.currentTarget.style.display = 'none')
                    const fallback = document.getElementById('fallbackLogo')
                    if (fallback) fallback.style.display = 'block'
                  }}
                />
              </a>
              <h2>Welcome Back</h2>
              <p>Enter your credentials to access your account</p>
            </header>
  
            <form action="404.html" method="GET" noValidate>
              <div className={s.formGroup}>
                <label htmlFor="email">Email Address</label>
                <input
                  type="email"
                  id="email"
                  name="email"
                  placeholder="you@example.com"
                  required
                  autoComplete="email"
                  aria-required="true"
                />
              </div>
  
              <div className={s.formGroup}>
                <label htmlFor="password">Password</label>
                <input
                  type="password"
                  id="password"
                  name="password"
                  placeholder="Enter your password"
                  required
                  autoComplete="current-password"
                  aria-required="true"
                />
              </div>
  
              <div className={s.forgotPassword}>
                <a href="404.html">Forgot password?</a>
              </div>
  
              <button type="submit" className={s.btnSignin}>
                Sign In
              </button>

            </form>
            
            <hr className={s.separator}></hr>

            <div className={s.infoTile} onClick={() => navigate(`/demos/semantic-search`)}>
              <div className={s.infoTileContent}>
                  <h3 className={s.infoTileTitle}>Featured Demo: Semantic Search</h3>
                  <p className={s.infoTileText}>Help users find what matters faster with meaning-aware search that understands intent, context, and natural language.</p>
              </div>
              <div className={s.infoTileImage}>
                  <img src="https://images.unsplash.com/photo-1464822759023-fed622ff2c3b?w=400&auto=format&fit=crop&q=80" alt="Homestead cabin in the woods"/>
              </div>
            </div>
          </div>
        </section>
      </div>
    )
  }
  
  export default NewLoginPage
  