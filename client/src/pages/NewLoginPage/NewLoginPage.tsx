import s from './NewLoginPage.module.scss';

const NewLoginPage = () => {
    return (
      <div className={s.container}>
        {/* Left Side: Image/Branding */}
        <section
          className={s.imageSection}
          role="img"
          aria-label="Application branding"
        >
          <div className={s.brandContent}>
            <h1></h1>
            <p></p>
          </div>
        </section>
  
        {/* Right Side: Sign In Form */}
        <section className={s.formSection}>
          <div className={s.formContainer}>
            <header className={s.formHeader}>
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
          </div>
        </section>
      </div>
    )
  }
  
  export default NewLoginPage
  