import { Box, Button, CardMedia, Container, TextField } from '@mui/material';
import KeyboardArrowDownIcon from '@mui/icons-material/KeyboardArrowDown';
import KeyboardArrowUpIcon from '@mui/icons-material/KeyboardArrowUp';
import validator from 'validator';

import React, { useState } from 'react';
import useProfileInfo from './hooks/useProfileInfo';
import { useEffect } from 'react';

const Profile = () => {
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [email, setEmail] = useState('');
  const [description, setDescription] = useState('');
  const [profileImage, setProfileImage] = useState('');
  const [addAppartmentIsOpen, setAddAppartmentIsOpen] = useState(false);

  const { data, getInfo } = useProfileInfo();
  useEffect(() => {
    getInfo().then((data) => {
      console.log(data);
      setFirstName(data.firstname);
      setLastName(data.lastname);
      setEmail(data.userName);
      if (data.image) {
        setProfileImage(data.image);
      }
      if (data.description) {
        setDescription(data.description);
      }
    });
  }, []);
  console.log(data);

 

  // fetch to get user's data, then put it in usestate as a default value. first name, last name etc.

  const profileImageChangeHandler = (event) => {
    console.log(event.target.files[0]);
    let fileURL = URL.createObjectURL(event.target.files[0]);
    setProfileImage(fileURL);
  };

  const saveChangesHandler = () => {
    if (!firstName || !lastName || !description) {
      if (validator.isEmpty(firstName)) {
      }
      if (validator.isEmpty(lastName)) {
      }
      if (!validator.isEmail(email)) {
      }
      return;
    }
  };
  return (
    <Container sx={{ m: 'auto', height: '370' }}>
      <Container
        sx={{
          display: 'flex',
          flexDirection: 'row',
          justifyContent: 'space-between',
          mb: 4,
          p: 2,
          border: '#000000',
        }}
      >
        <Box sx={{ display: 'flex', flexDirection: 'column' }}>
          <CardMedia
            component='img'
            height='300'
            width='300'
            image={profileImage}
            alt='Your Image'
            sx={{ mb: 1 }}
          />
          <Button variant='outlined' component='label'>
            Upload New image
            <input
              type='file'
              hidden
              accept='image/png, image/jpeg'
              onChange={profileImageChangeHandler}
            />
          </Button>
        </Box>
        <Box
          sx={{
            display: 'flex',
            flexDirection: 'column',
            justifyContent: 'space-between',
          }}
        >
          <Box sx={{ display: 'flex', justifyContent: 'space-between', my: 1 }}>
            <TextField
              required
              label='First Name'
              variant='outlined'
              onChange={(event) => setFirstName(event.target.value)}
              value={firstName}
              sx={{ mr: 1 }}
            />
            <TextField
              required
              label='Last Name'
              variant='outlined'
              value={lastName}
              onChange={(event) => setLastName(event.target.value)}
            />
          </Box>
          <TextField
            required
            fullWidth
            label='Email address'
            variant='outlined'
            type='email'
            onChange={(event) => setEmail(event.target.value)}
            value={email}
            sx={{ my: 1 }}
          />
          <TextField
            fullWidth
            label='Something About Yourself'
            variant='outlined'
            onChange={(event) => setDescription(event.target.value)}
            value={description}
            multiline
            minRows='2'
            sx={{ my: 1 }}
          />
          <br />
          <Button
            variant='contained'
            onClick={saveChangesHandler}
            sx={{ py: 1 }}
          >
            Save Changes
          </Button>
        </Box>
      </Container>
      <Container sx={{ mb: 4, p: 2 }}>
        <Button
          onClick={() => setAddAppartmentIsOpen((prevState) => !prevState)}
          endIcon={
            !addAppartmentIsOpen ? (
              <KeyboardArrowDownIcon />
            ) : (
              <KeyboardArrowUpIcon />
            )
          }
          sx={{ mb: 2, width: '100%', borderBottom: 1 }}
        >
          My appartment
        </Button>
        {addAppartmentIsOpen && (
          <Container
            sx={{
              display: 'flex',
              flexDirection: 'row',
              justifyContent: 'space-between',
            }}
          >
            <Box sx={{ width: '50%' }}>
              <TextField
                required
                fullWidth
                label='City'
                variant='outlined'
                onChange={(event) => console.log(event.target.value)}
                sx={{ my: 1 }}
              />
              <TextField
                required
                fullWidth
                label='Address'
                variant='outlined'
                onChange={(event) => console.log(event.target.value)}
                sx={{ my: 1 }}
              />
              <TextField
                required
                fullWidth
                label='Distance to Center'
                variant='outlined'
                onChange={(event) => console.log(event.target.value)}
                sx={{ my: 1 }}
              />
              <TextField
                required
                fullWidth
                label='Max number of Guests'
                variant='outlined'
                type='number'
                onChange={(event) => console.log(event.target.value)}
                sx={{ my: 1 }}
              />
              <TextField
                required
                fullWidth
                label='Description'
                variant='outlined'
                multiline
                minRows='2'
                onChange={(event) => console.log(event.target.value)}
                sx={{ my: 1 }}
              />
            </Box>
            <Box sx={{ display: 'flex', flexDirection: 'column' }}>
              <CardMedia
                component='img'
                height='300'
                width='300'
                image='https://picsum.photos/318/354'
                alt='Your Image'
                sx={{ mb: 1 }}
              />
              <Button variant='outlined' component='label'>
                Upload New image
                <input type='file' hidden accept='image/png, image/jpeg' />
              </Button>
            </Box>
          </Container>
        )}
      </Container>
    </Container>
  );
};

export default Profile;
