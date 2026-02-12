import './assets/styles/global.scss';
import { BrowserRouter as Router, Routes, Route, Navigate } from "react-router-dom";
import ScrollToTop from './assets/utils/ScrollToTop';
import HomePage from './pages/HomePage/HomePage';
import LoginPage from './pages/LoginPage/LoginPage';
import Sidebar from './components/Sidebar/Sidebar';
import Header from './components/Header/Header';
import { observer } from 'mobx-react-lite'
import RegistrationPage from './pages/RegistrationPage/RegistrationPage';
import { FC, useContext, useEffect } from 'react';
import { Context } from './Context';
import AccountPage from './pages/AccountPage/AccountPage';
import CreatePostPage from './pages/CreatePostPage/CreatePostPage';
import SinglePage from './pages/SinglePage/SinglePage';
import SearchPage from './pages/SearchPage/SearchPage';
import SemanticSearch from './pages/SemanticSearch/SemanticSearch';
import OldHomePage from './pages/OldHomePage/OldHomePage';
import BlogPage from './pages/BlogPage/BlogPage';
import NewLoginPage from './pages/NewLoginPage/NewLoginPage';

const App: FC = () => {
  const { store } = useContext(Context)

  useEffect(() => {
    if (localStorage.getItem('token')) {
      store.checkAuth()
    }
  }, [])

  return (
    <>
      <Router>
        {/* <ScrollToTop /> */}
        {/* <Header /> */}

        {/* <Sidebar> */}
          <Routes>
            <Route path="/posts/search/:id" element={<SearchPage type={'search'} />} />
            <Route path="/posts/semantic-search" element={<SemanticSearch/>} />
            <Route path="/posts/search/:id" element={<SearchPage type={'search'} />} />
            <Route path="/posts/category/:id" element={<SearchPage type={'category'} />} />
            <Route path="/post/:id" element={<SinglePage />} />
            <Route path="/create-post" element={<CreatePostPage />} />
            <Route path="/account" element={<AccountPage />} />
            {/* <Route path="/" element={<LoginPage />} /> */}
            <Route path="/" element={<NewLoginPage />} />
            <Route path="/registration" element={<RegistrationPage />} />
            <Route path="/normal-search" element={<OldHomePage/>} />
            <Route path="/demos/semantic-search" element={<BlogPage/>} />
            <Route path="/demos" element={<HomePage />} />
            <Route path="*" element={<Navigate to="/" />} />
          </Routes>
        {/* </Sidebar> */}
      </Router>
    </>
  );
}

export default observer(App);
