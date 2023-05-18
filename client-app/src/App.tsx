import React, { useEffect, useState } from 'react';
import logo from './logo.svg';
import 'semantic-ui-css/semantic.min.css'
import './App.css';
import axios from 'axios';
import { Button, Header, List, ListContent } from 'semantic-ui-react';

function App() {
  // Variable to store activities and a function to set the activities when we get them back from our API. The parameter represents the default value. In this case, the default value is a list
  const [activities, setActivities] = useState([]);

  // Allows to perform side effects in function components once they're mounted/updated
  // useEffect requires some dependencies in order to avoid an infinite loop since the effect is called whenever the component is updated and since we're updating the component inside it.
  // 
  useEffect(() => {
    axios.get('http://localhost:5000/api/activities')
      .then(response => {
        setActivities(response.data)
      })

    // This array represents the dependencies that will make it so the effect runs only once
  }, [])

  return (
    <div>
      <Header as='h2' icon='users' content='Reactivities' />
      <List>
        {activities.map((activity: any) => (
          <List.Item key={activity.id}>
            {activity.title}
          </List.Item>
        ))}
      </List>
      <Button content='test'/>
    </div>
  );
}

export default App;
