import React from 'react'
import Alert from 'react-bootstrap/Alert';

function LogoutAlert() {
  return (
    <Alert variant="danger" onClose={() => setShow(false)} dismissible>
        <Alert.Heading>You have been logged out! </Alert.Heading>
      </Alert>
  )
}

export default LogoutAlert