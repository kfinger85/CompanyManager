import React from "react";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import "./App.css";
import Navbar from "./components/Navbar";
import Home from "./pages";
import About from "./pages/about";
import Projects from "./pages/projects";
import Qualifications from "./pages/qualifications";
import Workers from "./pages/workers";
import ErrorBoundary from "./components/ErrorBoundary";
import { useState } from "react";

function App() {
  const [modalOpen, setModalOpen] = useState(false);
  const [projectModalOpen, setProjectModalOpen] = useState(false);

  return (
    <ErrorBoundary>
      <Router>
        <Navbar />
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/about" element={<About />} />
          <Route
            path="/workers/*"
            element={
              <Workers
                modalOpen={modalOpen}
                setModalOpen={setModalOpen}
                projectModalOpen={projectModalOpen}
                setProjectModalOpen={setProjectModalOpen}
              />
            }
          />
          <Route
            path="/projects/*"
            element={
              <Projects
                projectModalOpen={projectModalOpen}
                setProjectModalOpen={setProjectModalOpen}
              />
            }
          />
          <Route
            path="/qualifications/*"
            element={
              <Qualifications
                modalOpen={modalOpen}
                setModalOpen={setModalOpen}
              />
            }
          />
        </Routes>
      </Router>
    </ErrorBoundary>
  );
}

export default App;
