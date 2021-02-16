import React, { useState } from 'react';
import {Projects} from './Pages/Projects';

import {
  BrowserRouter as Router,
  Switch,
  Route,
  Link,
  useRouteMatch,
  useParams
} from "react-router-dom";

function GlobalMessage({message}){
  if(message !== ""){
    let classes=`alert ${message.class}`;
    return <div className={classes} role="alert">
    {message.message}
  </div>;
  }
  else{
    return <div></div>
  }
}

export default function App() {
  const [count, setCount] = useState(0);
  const [message, setMessage] = useState("");
  return (
    <>
      <nav className="navbar navbar-expand-lg navbar-light bg-light">
        <div className="container">
          <a className="navbar-brand" href="#">Project Speedy</a>
        </div>
      </nav>

      <div className="container">
        <div className="row">
          <div className="col">
          <GlobalMessage message={message}/>
          <p>You clicked {count} times</p>
          <button onClick={() => setCount(count + 1)}>
            Click me
          </button>
            <Router>
              <div>
                <ul>
                  <li>
                    <Link to="/">Home</Link>
                  </li>
                  <li>
                    <Link to="/about">About</Link>
                  </li>
                  <li>
                    <Link to="/topics">Topics</Link>
                  </li>
                </ul>

                <Switch>
                <Route path="/project">
                    <ProjectSection />
                  </Route>
                  <Route path="/about">
                    <About />
                  </Route>
                  <Route path="/topics">
                    <Topics />
                  </Route>
                  <Route path="/">
                    <Projects globalMessage={(alertMessage) => setMessage(alertMessage)} />
                  </Route>
                </Switch>
              </div>
            </Router>
          </div>
        </div>
      </div>
    </>
  );
}

function About() {
  return <h2>Abouts</h2>;
}

function ProjectSection() {
  let match = useRouteMatch();

  return (
    <div>
      <Switch>
      <Route path={`${match.path}/:projectId/bet/:betId`}>
        <h3>Bet</h3>
        </Route>
        <Route path={`${match.path}/:projectId/bet/`}>
        <h3>Bet id not supplied</h3>
        </Route>
        <Route path={`${match.path}/:projectId`}>
        <h3>Project</h3>
        </Route>
        <Route path={match.path}>
          <h3>Project Id not supplied</h3>
        </Route>
      </Switch>
    </div>
  );
}

function Topics() {
  let match = useRouteMatch();

  return (
    <div>
      <h2>Topics</h2>

      <ul>
        <li>
          <Link to={`${match.url}/components`}>Components</Link>
        </li>
        <li>
          <Link to={`${match.url}/props-v-state`}>
            Props v. State
          </Link>
        </li>
      </ul>

      {/* The Topics page has its own <Switch> with more routes
          that build on the /topics URL path. You can think of the
          2nd <Route> here as an "index" page for all topics, or
          the page that is shown when no topic is selected */}
      <Switch>
        <Route path={`${match.path}/:topicId`}>
          <Topic />
        </Route>
        <Route path={match.path}>
          <h3>Please select a topic.</h3>
        </Route>
      </Switch>
    </div>
  );
}

function Topic() {
  let { topicId } = useParams();
  return <h3>Requested topic ID: {topicId}</h3>;
}

