import { Box, Button, CardMedia, Container, TextField } from '@mui/material';
import KeyboardArrowDownIcon from '@mui/icons-material/KeyboardArrowDown';
import KeyboardArrowUpIcon from '@mui/icons-material/KeyboardArrowUp';
import validator from 'validator';
import React, { useState } from 'react';
import useProfileInfo from './hooks/useProfileInfo';

const Profile = () => {
  const profileData = useProfileInfo();
  const [firstName, setFirstName] = useState();
  const [lastName, setLastName] = useState();
  const [email, setEmail] = useState();
  const [description, setDescription] = useState();

  const [profileImage, setProfileImage] = useState(
    'https://picsum.photos/318/354'
  );
  const [addAppartmentIsOpen, setAddAppartmentIsOpen] = useState(false);
  console.log(profileData);

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
              sx={{ mr: 1 }}
            />
            <TextField
              required
              label='Last Name'
              variant='outlined'
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
            sx={{ my: 1 }}
          />
          <TextField
            fullWidth
            label='Something About Yourself'
            variant='outlined'
            onChange={(event) => setDescription(event.target.value)}
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
