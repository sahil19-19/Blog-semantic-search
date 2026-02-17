import s from './NewLoginPage.module.scss';
import logo from '../../assets/img/logo.png';
import { Link } from 'react-router-dom';
import platHomePageImage from '../../assets/img/plat-homepage.png';

const NewLoginPage = () => {
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
              <Link className={s.brand} to="/demos">
                <img src={logo} alt="Application Logo" className={s.logoImg} 
                    onError={(e) => {
                    (e.currentTarget.style.display = 'none')
                    const fallback = document.getElementById('fallbackLogo')
                    if (fallback) fallback.style.display = 'block'
                  }}/>
                </Link>
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
                Login
              </button>
            </form>
          </div>
        </section>
      </div>
    )
  }
  
  export default NewLoginPage
  